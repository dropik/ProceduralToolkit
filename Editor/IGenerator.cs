using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit
{
    public interface IGenerator
    {
        IEnumerable<Vector3> Vertices { get; }
        IEnumerable<int> Triangles { get; }
    }
}
