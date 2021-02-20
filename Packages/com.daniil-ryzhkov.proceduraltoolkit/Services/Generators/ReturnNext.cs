using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnNext
    {
        private readonly IEnumerator<Vector3> inputVerticesEnumerator;
        protected RowDuplicatorContext Context { get; private set; }

        public ReturnNext(IEnumerator<Vector3> inputVerticesEnumerator, RowDuplicatorContext context)
        {
            this.inputVerticesEnumerator = inputVerticesEnumerator;
            this.Context = context;
        }

        public IRowDuplicatorState NextState { get; set; }

        public bool MoveNext()
        {
            if (!inputVerticesEnumerator.MoveNext())
            {
                return false;
            }

            var vertex = inputVerticesEnumerator.Current;
            HandleVertex(vertex);
            Context.Current = vertex;

            Context.Column++;
            if (Context.Column >= Context.ColumnsInRow)
            {
                Context.Column = 0;
                Context.Row++;
                Context.State = NextState;
            }

            return true;
        }

        protected virtual void HandleVertex(Vector3 vertex) { }
    }
}
