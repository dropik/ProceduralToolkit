using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnNext
    {
        protected IEnumerator<Vector3> InputVerticesEnumerator { get; private set; }
        protected RowDuplicatorContext Context { get; private set; }

        public ReturnNext(IEnumerator<Vector3> inputVerticesEnumerator, RowDuplicatorContext context)
        {
            this.InputVerticesEnumerator = inputVerticesEnumerator;
            this.Context = context;
        }

        public IRowDuplicatorState NextState { get; set; }

        public bool MoveNext()
        {
            PreMoveNext();

            if (!InputVerticesEnumerator.MoveNext())
            {
                return false;
            }

            var vertex = InputVerticesEnumerator.Current;
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

        protected virtual void PreMoveNext() { }

        protected virtual void HandleVertex(Vector3 vertex) { }
    }
}
