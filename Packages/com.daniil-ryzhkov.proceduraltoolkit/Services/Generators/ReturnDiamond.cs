using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnDiamond : BaseReturnVertex
    {
        public ReturnDiamond(DiamondContext context) : base(context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex)
        {
            return vertex + Context.XZShift;
        }

        protected override void SetCurrentWithVertex(Vector3 vertex)
        {
            Context.OriginalVertices[Context.Column] = vertex;
            Context.Current = vertex + Context.XZShift;
        }
    }
}
