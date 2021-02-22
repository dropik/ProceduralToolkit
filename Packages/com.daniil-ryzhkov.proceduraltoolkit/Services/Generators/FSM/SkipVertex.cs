using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class SkipVertex : BaseDiamondTilingState
    {
        public SkipVertex(FSMContext context) : base(context) { }

        protected override Vector3? GetResultVertex(Vector3 vertex) => null;
    }
}