using ProceduralToolkit.Models.FSM;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class TransitionBuilder : ITransitionBuilder
    {
        private readonly Transition transition;
        private readonly IStateBuilder builder;

        public TransitionBuilder(IStateBuilder builder, Transition transition)
        {
            this.builder = builder;
            this.transition = transition;
        }

        public IStateBuilder SetNext(string name)
        {
            transition.NextState = name;
            return builder;
        }
    }
}
