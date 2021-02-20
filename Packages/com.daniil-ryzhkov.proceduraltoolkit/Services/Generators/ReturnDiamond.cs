using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnDiamond : BaseDiamondTilingState
    {
        public ReturnDiamond(DiamondTilingContext context) : base(context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex)
        {
            return new Vector3(vertex.x, 0, vertex.z) + Context.XZShift;
        }
    }
}
