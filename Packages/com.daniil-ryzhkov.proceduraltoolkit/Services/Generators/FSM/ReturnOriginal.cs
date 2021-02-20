using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ReturnOriginal : BaseDiamondTilingState
    {
        public ReturnOriginal(DiamondTilingContext context) : base(context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex) => vertex;
    }
}