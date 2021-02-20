using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ResetAndReturnNext : ReturnNext
    {
        public ResetAndReturnNext(
            IEnumerator<Vector3> inputVerticesEnumerator,
            RowDuplicatorContext context)
        : base(inputVerticesEnumerator, context) { }

        protected override void PreMoveNext()
        {
            if (Context.Column == 0)
            {
                InputVerticesEnumerator.Reset();
            }
        }
    }
}
