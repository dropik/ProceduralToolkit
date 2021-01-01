using UnityEngine;
using ProceduralToolkit.Api;
using System.Collections.Generic;

namespace ProceduralToolkit.Landscape
{
    public class PlaneSettings : MonoBehaviour, IGenerator
    {
        [Min(0.0f)]
        public float length;
        [Min(0.0f)]
        public float width;

        private IGenerator generator;

        public void Reset()
        {
            length = 1f;
            width = 1f;
        }

        public void OnValidate()
        {
            generator = new PlaneGenerator(length, width);
        }

        public IEnumerable<Vector3> Vertices => generator.Vertices;

        public IEnumerable<int> Triangles => generator.Triangles;
    }
}
