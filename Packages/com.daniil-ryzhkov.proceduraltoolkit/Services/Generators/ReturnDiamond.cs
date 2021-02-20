using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnDiamond : BaseDiamondTilingState
    {
        public ReturnDiamond(DiamondTilingContext context) : base(context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex) => vertex + Context.XZShift;
    }
}
