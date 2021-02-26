using ProceduralToolkit.Models.FSM;
using System;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StateBuilder : IStateBuilder
    {
        private readonly FSMContext context;

        public StateBuilder(FSMContext context)
        {
            this.context = context;
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
            return new TransitionBehaviour(output, context, new List<Transition>(), (state, transition) => new TransitionBuilder(state, transition))
            {
                VertexPreprocessor = preprocessor
            };
        }

        public IState BuildState()
        {
            return default;
        }
    }
}
