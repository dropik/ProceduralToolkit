using ProceduralToolkit.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class TransitionBehaviour : ITransitionBehaviour
    {
        private readonly IStateOutput output;
        private readonly FSMContext context;
        private readonly IList<Transition> transitions;
        private readonly Func<ITransitionBehaviour, Transition, ITransitionBuilder> transitionBuilderProvider;
        private readonly IEnumerable<Transition> transitionsEnumerable;
        private readonly IMachine machine;

        private ITransitionBehaviour defaultNextState;

        public TransitionBehaviour(FSMContext context, IEnumerable<Transition> transitions, IMachine machine)
        {
            this.context = context;
            this.transitionsEnumerable = transitions;
            this.machine = machine;
        }

        public IVertexPreprocessor VertexPreprocessor { get; set; }

        public void Execute()
        {
            context.Column++;
            foreach (var transition in transitionsEnumerable)
            {
                if (transition.Condition())
                {
                    machine.SetState(transition.NextStateName);
                    TryResetColumn(transition);
                    break;
                }
            }
        }

        public IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            VertexPreprocessor?.Process(vertex);
            context.Column++;
            CheckTransitions();
            return output.GetOutputFor(vertex);
        }

        private void CheckTransitions()
        {
            foreach (var transition in WithDefaultTransitions)
            {
                if (transition.Condition())
                {
                    HandleConditionHit(transition);
                    break;
                }
            }
        }

        private void HandleConditionHit(Transition transition)
        {
            SwitchState(transition);
            TryResetColumn(transition);
        }

        private void SwitchState(Transition transition)
        {
            context.State = transition.NextState;
        }

        private void TryResetColumn(Transition transition)
        {
            if (transition.ZeroColumn)
            {
                context.Column = 0;
            }
        }

        private IEnumerable<Transition> WithDefaultTransitions => transitions.Concat(new Transition[]
        {
            new Transition()
            {
                Condition = () => true,
                NextState = defaultNextState,
                ZeroColumn = false
            }
        });

        public ITransitionBuilder On(Func<bool> condition)
        {
            var newTransition = new Transition()
            {
                Condition = condition
            };
            transitions.Add(newTransition);
            return transitionBuilderProvider.Invoke(this, newTransition);
        }

        public ITransitionBehaviour SetDefaultNext(ITransitionBehaviour next)
        {
            defaultNextState = next;
            return this;
        }

        public ITransitionBehaviour DoNotZeroColumn()
        {
            transitions[transitions.Count - 1].ZeroColumn = false;
            return this;
        }
    }
}
