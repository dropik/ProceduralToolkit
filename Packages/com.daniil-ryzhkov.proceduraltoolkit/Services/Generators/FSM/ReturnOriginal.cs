using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class ReturnOriginal : BaseState
    {
        public ReturnOriginal(FSMSettings settings) : base(settings) { }

        protected override IEnumerable<Vector3> GetResultVertices(Vector3 vertex)
        {
            yield return vertex;
        }
    }
}