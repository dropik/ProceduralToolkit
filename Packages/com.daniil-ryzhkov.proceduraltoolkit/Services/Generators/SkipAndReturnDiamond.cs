using System.Collections.Generic;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class SkipAndReturnDiamond : IState
    {
        private readonly IEnumerator<Vector3> inputVerticesEnumerator;
        private readonly DiamondContext context;

        public SkipAndReturnDiamond(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
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
            context.Current = vertex + context.XZShift;
            context.Column++;
            context.State = NextState;
        }
    }
}