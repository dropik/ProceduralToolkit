using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class TransitionBehaviour : ITransitionBehaviour
    {
        private readonly FSMContext context;
        private readonly IEnumerable<Transition> transitions;
        private readonly IMachine machine;


        public TransitionBehaviour(FSMContext context, IEnumerable<Transition> transitions, IMachine machine)
        {
            this.context = context;
            this.transitions = transitions;
            this.machine = machine;
        }

        public void Execute()
        {
            context.Column++;
            CheckTransitions();
        }

        private void CheckTransitions()
        {
            foreach (var transition in transitions)
            {
                if (transition.Condition())
                {
                    machine.SetState(transition.NextState);
                    TryResetColumn(transition);
                    break;
                }
            }
        }

        private void TryResetColumn(Transition transition)
        {
            if (transition.ZeroColumn)
            {
                context.Column = 0;
            }
        }
    }
}
