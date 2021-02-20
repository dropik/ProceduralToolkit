using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnNext
    {
        private readonly IEnumerator<Vector3> inputVerticesEnumerator;
        private readonly RowDuplicatorContext context;

        public ReturnNext(IEnumerator<Vector3> inputVerticesEnumerator, RowDuplicatorContext context)
        {
            this.inputVerticesEnumerator = inputVerticesEnumerator;
            this.context = context;
        }

        public IRowDuplicatorState NextState { get; set; }

        public bool MoveNext()
        {
            if (!inputVerticesEnumerator.MoveNext())
            {
                return false;
            }

            context.Current = inputVerticesEnumerator.Current;

            context.Column++;
            if (context.Column >= context.ColumnsInRow)
            {
                context.Column = 0;
                context.Row++;
                context.State = NextState;
            }

            return true;
        }
    }
}
