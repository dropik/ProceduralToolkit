using ProceduralToolkit.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class State : IState
    {
        private readonly IStateBehaviour stateBehaviour;
        private readonly FSMContext context;
        private readonly IList<Transition> transitions;
        private readonly Func<Transition, ITransitionBuilder> transitionBuilderProvider;

        private IState defaultNextState;

        public State(IStateBehaviour stateBehaviour, FSMContext context, IList<Transition> transitions, Func<Transition, ITransitionBuilder> transitionBuilderProvider)
        {
            this.stateBehaviour = stateBehaviour;
            this.context = context;
            this.transitions = transitions;
            this.transitionBuilderProvider = transitionBuilderProvider;
        }

        public IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            foreach (var transition in WithDefaultTransitions)
            {
                if (transition.Condition())
                {
                    context.State = transition.NextState;
                    return stateBehaviour.MoveNext(vertex);
                }
            }

            return default;
        }

        private IEnumerable<Transition> WithDefaultTransitions => transitions.Concat(new Transition[]
        {
            new Transition()
            {
                Condition = () => true,
                NextState = defaultNextState
            }
        });

        public ITransitionBuilder On(Func<bool> condition)
        {
            var newTransition = new Transition()
            {
                Condition = condition
            };
            transitions.Add(newTransition);
            return transitionBuilderProvider.Invoke(newTransition);
        }

        public IState SetDefaultNext(IState next)
        {
            defaultNextState = next;
            return this;
        }
    }
}
