using ProceduralToolkit.Models.FSM;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class State : IState
    {
        private readonly IStateOutput output;
        private readonly FSMContext context;
        private readonly IList<Transition> transitions;
        private readonly Func<IState, Transition, ITransitionBuilder> transitionBuilderProvider;

        private IState defaultNextState;

        public State(IStateOutput output,
                     FSMContext context,
                     IList<Transition> transitions,
                     Func<IState, Transition, ITransitionBuilder> transitionBuilderProvider)
        {
            this.output = output;
            this.context = context;
            this.transitions = transitions;
            this.transitionBuilderProvider = transitionBuilderProvider;
        }

        public IVertexPreprocessor VertexPreprocessor { get; set; }

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

        public IState SetDefaultNext(IState next)
        {
            defaultNextState = next;
            return this;
        }

        public IState DoNotZeroColumn()
        {
            transitions[transitions.Count - 1].ZeroColumn = false;
            return this;
        }
    }
}
