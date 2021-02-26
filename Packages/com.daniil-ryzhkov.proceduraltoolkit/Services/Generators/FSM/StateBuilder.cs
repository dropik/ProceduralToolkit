using ProceduralToolkit.Models.FSM;
using System;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StateBuilder : IStateBuilder
    {
        private readonly FSMContext context;
        private readonly List<Transition> transitions;

        public StateBuilder(FSMContext context)
        {
            this.context = context;
            output = new OutputOriginal();
            transitions = new List<Transition>();
        }

        private IVertexPreprocessor preprocessor;
        private IStateOutput output;

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

        public ITransitionBehaviour Build()
        {
            return default;
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

        public IState BuildState(IMachine machine)
        {
            var transitionBehaviour = new TransitionBehaviour(context, transitions, machine);
            return new State(output, transitionBehaviour)
            {
                VertexPreprocessor = preprocessor
            };
        }
    }
}
