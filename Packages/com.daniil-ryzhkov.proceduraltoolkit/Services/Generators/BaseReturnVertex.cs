using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public abstract class BaseReturnVertex : IState
    {
        private readonly IEnumerator<Vector3> inputVerticesEnumerator;
        private readonly DiamondContext context;
        protected DiamondContext Context => context;

        public BaseReturnVertex(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
        {
            this.inputVerticesEnumerator = inputVerticesEnumerator;
            this.context = context;
        }

        public IState NextState { get; set; }

        public void MoveNext()
        {
            inputVerticesEnumerator.MoveNext();
            SetCurrentWithVertex(inputVerticesEnumerator.Current);
            context.Column++;
            if (context.Column >= context.Length)
            {
                context.Column = 0;
                context.Row++;
                context.State = NextState;
            }
        }

        protected abstract void SetCurrentWithVertex(Vector3 vertex);
    }
}
