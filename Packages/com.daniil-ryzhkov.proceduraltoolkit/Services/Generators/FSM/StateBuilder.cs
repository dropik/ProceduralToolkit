using ProceduralToolkit.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StateBuilder : IStateBuilder
    {
        private readonly FSMContext context;
        private readonly List<Transition> transitions;

        private IVertexPreprocessor preprocessor;
        private IStateOutput output;
        private bool hasDefault;
        private string defaultStateName;

        public StateBuilder(FSMContext context)
        {
            this.context = context;
            output = new OutputOriginal();
            transitions = new List<Transition>();
        }

        public IStateBuilder ConfigureOutput<TOutput>(TOutput output) where TOutput : IStateOutput
        {
            this.output = output;
            return this;
        }

        public IStateBuilder ConfigurePreprocessor<TPreprocessor>(TPreprocessor preprocessor) where TPreprocessor : IVertexPreprocessor
        {
            this.preprocessor = preprocessor;
            return this;
        }

        public ITransitionBuilder On(Func<bool> condition)
        {
            var transition = new Transition()
            {
                Condition = condition
            };
            transitions.Add(transition);
            return new TransitionBuilder(this, transition);
        }

        public IStateBuilder DoNotZeroColumn()
        {
            transitions[transitions.Count - 1].ZeroColumn = false;
            return this;
        }

        public IStateBuilder SetDefaultState(string name)
        {
            defaultStateName = name;
            hasDefault = true;
            return this;
        }

        public IState BuildState(IMachine machine)
        {
            var transitionBehaviour = new TransitionBehaviour(context, WithDefaultTransitions, machine);
            return new State(output, transitionBehaviour)
            {
                VertexPreprocessor = preprocessor
            };
        }

        private IEnumerable<Transition> WithDefaultTransitions => hasDefault ? transitions.Concat(new Transition[]
        {
            new Transition()
            {
                Condition = () => true,
                NextState = defaultStateName,
                ZeroColumn = false
            }
        }) : transitions;
    }
}
