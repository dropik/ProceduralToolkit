using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Api
{
    public interface IGenerator
    {
        IEnumerable<Vector3> Vertices { get; }
        IEnumerable<int> Triangles { get; }
    }
}
