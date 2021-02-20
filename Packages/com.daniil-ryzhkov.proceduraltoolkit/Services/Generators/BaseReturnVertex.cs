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

        public BaseReturnVertex(DiamondContext context)
        {
            this.context = context;
        }

        public IState StateWhenRowContinues { get; set; }
        public IState StateWhenEndedRow { get; set; }

        public void MoveNext()
        {
            inputVerticesEnumerator.MoveNext();
            SetCurrentWithVertex(inputVerticesEnumerator.Current);
            context.Column++;
            if (context.Column >= context.Length)
            {
                context.Column = 0;
                context.Row++;
                context.State = StateWhenEndedRow;
            }
        }

        protected abstract void SetCurrentWithVertex(Vector3 vertex);

        public Vector3? MoveNext(Vector3 vertex)
        {
            SwitchState();
            return GetResultVertex(vertex);
        }

        private void SwitchState()
        {
            Context.Column++;
            if (Context.Column >= Context.Length)
            {
                Context.Column = 0;
                Context.State = StateWhenEndedRow;
            }
            else
            {
                Context.State = StateWhenRowContinues;
            }
        }

        protected abstract Vector3? GetResultVertex(Vector3 vertex);
    }
}
