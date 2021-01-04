using ProceduralToolkit.Api;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Components.GeneratorSettings
{
    public abstract class BaseSettings : MonoBehaviour, IGenerator
    {
        private IGenerator generator;

        public event System.Action GeneratorUpdated;

        public void OnValidate()
        {
            generator = CreateGenerator();
            GeneratorUpdated?.Invoke();
        }

        protected abstract IGenerator CreateGenerator();

        public IEnumerable<Vector3> Vertices => generator.Vertices;

        public IEnumerable<int> Triangles => generator.Triangles;
    }
}
