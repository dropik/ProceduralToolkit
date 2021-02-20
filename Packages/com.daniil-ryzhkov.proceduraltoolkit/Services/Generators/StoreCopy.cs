using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class StoreCopy : ReturnNext
    {
        public StoreCopy(
            IEnumerator<Vector3> inputVerticesEnumerator,
            RowDuplicatorContext context)
        : base(inputVerticesEnumerator, context) { }

        protected override void HandleVertex(Vector3 vertex)
        {
            if (Context.ColumnsInRow > 0)
            {
                Context.VerticesCopies[Context.Column] = vertex;
            }
        }
    }
}
