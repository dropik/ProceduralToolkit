using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IStateBehaviour
    {
        IEnumerable<Vector3> MoveNext(Vector3 vertex);
    }
}