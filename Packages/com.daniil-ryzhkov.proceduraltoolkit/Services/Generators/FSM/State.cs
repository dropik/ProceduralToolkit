using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class State : IState
    {
        private readonly IStateOutput output;
        private readonly ITransitionBehaviour transitionBehaviour;

        public State(IStateOutput output, ITransitionBehaviour transitionBehaviour)
        {
            this.output = output;
            this.transitionBehaviour = transitionBehaviour;
        }

        public IVertexPreprocessor VertexPreprocessor { get; set; }

        public IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            VertexPreprocessor?.Process(vertex);
            transitionBehaviour.Execute();
            return output.GetOutputFor(vertex);
        }
    }
}