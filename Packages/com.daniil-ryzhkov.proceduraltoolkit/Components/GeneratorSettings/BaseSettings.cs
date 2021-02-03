using ProceduralToolkit.Services.Generators;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Components.GeneratorSettings
{
    public abstract class BaseSettings : MonoBehaviour, IGenerator
    {
        private IGenerator generator;

        public event Action GeneratorUpdated;

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
