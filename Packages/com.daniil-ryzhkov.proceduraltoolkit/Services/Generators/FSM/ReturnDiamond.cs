using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ReturnDiamond : BaseState
    {
        public ReturnDiamond(FSMSettings settings) : base(settings) { }

        protected override IEnumerable<Vector3> GetResultVertices(Vector3 vertex)
        {
            yield return new Vector3(vertex.x, 0, vertex.z) + Settings.FSMContext.DiamondTilingContext.XZShift;
        }
    }
}
