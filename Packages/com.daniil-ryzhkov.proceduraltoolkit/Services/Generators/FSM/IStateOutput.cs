using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IStateOutput
    {
        IEnumerable<Vector3> GetOutputFor(Vector3 vertex);
    }
}