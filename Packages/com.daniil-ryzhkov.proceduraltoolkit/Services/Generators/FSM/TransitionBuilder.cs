using ProceduralToolkit.Models.FSM;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class TransitionBuilder : ITransitionBuilder
    {
        private readonly ITransitionBehaviour state;
        private readonly Transition transition;
        private readonly IStateBuilder builder;

        public TransitionBuilder(ITransitionBehaviour state, Transition transition)
        {
            this.state = state;
            this.transition = transition;
        }

        public TransitionBuilder(IStateBuilder builder, Transition transition)
        {
            this.builder = builder;
            this.transition = transition;
        }

        public ITransitionBehaviour SetNext(ITransitionBehaviour next)
        {
            transition.NextState = next;
            return state;
        }

        public IStateBuilder SetNext(string name)
        {
            transition.NextStateName = name;
            return builder;
        }
    }
}
