using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class OutputOriginal : IStateOutput
    {
        public IEnumerable<Vector3> GetOutputFor(Vector3 vertex)
        {
            yield return vertex;
        }
    }
}
