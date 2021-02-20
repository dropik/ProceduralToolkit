using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnDiamond : BaseReturnVertex
    {
        public ReturnDiamond(
            IEnumerator<Vector3> inputVerticesEnumerator,
            DiamondContext context)
        : base(inputVerticesEnumerator, context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex)
        {
            throw new System.NotImplementedException();
        }

        protected override void SetCurrentWithVertex(Vector3 vertex)
        {
            Context.OriginalVertices[Context.Column] = vertex;
            Context.Current = vertex + Context.XZShift;
        }
    }
}
