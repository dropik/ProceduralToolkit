using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit
{
    public abstract class BaseShapeGeneratorSettings : ScriptableObject, IGenerator
    {
        private IGenerator generator;

        public IEnumerable<Vector3> Vertices => generator.Vertices;
        public IEnumerable<int> Triangles => generator.Triangles;

        private void Awake()
        {
            generator = CreateGenerator();
        }

        private void OnValidate()
        {
            generator = CreateGenerator();
        }

        protected abstract IGenerator CreateGenerator();
    }
}