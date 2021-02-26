using ProceduralToolkit.Models.FSM;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class TransitionBuilder : ITransitionBuilder
    {
        private readonly ITransitionBehaviour state;
        private readonly Transition transition;

        public TransitionBuilder(ITransitionBehaviour state, Transition transition)
        {
            this.state = state;
            this.transition = transition;
        }

        public ITransitionBehaviour SetNext(ITransitionBehaviour next)
        {
            transition.NextState = next;
            return state;
        }
    }
}
