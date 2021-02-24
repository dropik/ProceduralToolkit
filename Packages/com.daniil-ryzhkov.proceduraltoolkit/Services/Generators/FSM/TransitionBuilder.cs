using ProceduralToolkit.Models.FSM;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class TransitionBuilder : ITransitionBuilder
    {
        private readonly IState state;
        private readonly Transition transition;

        public TransitionBuilder(IState state, Transition transition)
        {
            this.state = state;
            this.transition = transition;
        }

        public IState SetNext(IState next)
        {
            transition.NextState = next;
            return state;
        }
    }
}
