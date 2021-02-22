using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ReturnOriginal : BaseDiamondTilingState
    {
        public ReturnOriginal(FSMContext context) : base(context) { }

        protected override IEnumerable<Vector3> GetResultVertices(Vector3 vertex)
        {
            yield return vertex;
        }
    }
}