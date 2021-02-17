using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnDiamond : IState
    {
        private readonly IEnumerator<Vector3> inputVerticesEnumerator;
        private readonly DiamondContext context;

        public ReturnDiamond(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
        {
            this.inputVerticesEnumerator = inputVerticesEnumerator;
            this.context = context;
        }

        public IState NextState { get; set; }

        public void MoveNext()
        {
            inputVerticesEnumerator.MoveNext();
            var vertex = inputVerticesEnumerator.Current;
            context.OriginalVertices[context.Column] = vertex;
            context.Current = vertex + context.XZShift;

            context.Column++;
            if (context.Column >= context.Length)
            {
                context.Row++;
                context.State = NextState;
            }
        }
    }
}
