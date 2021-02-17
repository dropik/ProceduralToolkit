using System.Collections.Generic;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnOriginal : BaseReturnVertex
    {
        public ReturnOriginal(
            IEnumerator<Vector3> inputVerticesEnumerator,
            DiamondContext context)
        : base(inputVerticesEnumerator, context) { }

        protected override void SetCurrentWithVertex(Vector3 vertex)
        {
            Context.Current = vertex;
        }
    }
}