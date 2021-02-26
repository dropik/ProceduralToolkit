using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class Machine : IMachine
    {
        private readonly IDictionary<string, IState> states;
        private readonly Func<IStateBuilder> stateBuilderProvider;

        private IState currentState;

        public Machine(IDictionary<string, IState> states, Func<IStateBuilder> stateBuilderProvider)
        {
            this.states = states;
            this.stateBuilderProvider = stateBuilderProvider;
        }

        public void AddState(string name, Action<IStateBuilder> build)
        {
            var stateBuilder = stateBuilderProvider.Invoke();
            build?.Invoke(stateBuilder);
            states.Add(name, stateBuilder.BuildState(this));
        }

        public void SetState(string name)
        {
            currentState = states[name];
        }

        public IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            return currentState?.MoveNext(vertex) ?? new Vector3[0];
        }
    }
}