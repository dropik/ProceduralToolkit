using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class SkipVertex : BaseDiamondTilingState
    {
        public SkipVertex(DiamondTilingContext context) : base(context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex) => null;
    }
}