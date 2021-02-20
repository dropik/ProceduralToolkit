using System.Collections.Generic;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class CalculateXZShift : IState
    {
        private readonly IEnumerator<Vector3> inputVerticesEnumerator;
        private readonly DiamondContext context;

        public CalculateXZShift(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
        {
            this.inputVerticesEnumerator = inputVerticesEnumerator;
            this.context = context;
        }

        public IState NextState { get; set; }

        public void MoveNext()
        {
            inputVerticesEnumerator.MoveNext();
            context.OriginalVertices[context.Column] = inputVerticesEnumerator.Current;

            context.Column++;

            inputVerticesEnumerator.MoveNext();
            var vertex = inputVerticesEnumerator.Current;
            context.OriginalVertices[context.Column] = vertex;
            context.XZShift = (context.First - vertex) / 2;
            context.Current = vertex + context.XZShift;
            context.Column++;
            context.State = NextState;
        }
    }
}