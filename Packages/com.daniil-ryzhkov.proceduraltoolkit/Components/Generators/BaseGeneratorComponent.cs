using System;
using System.Collections.Generic;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    public abstract class BaseGeneratorComponent : MonoBehaviour, IGeneratorComponent
    {
        private IGenerator Generator
        {
            get
            {
                if (generator == null)
                {
                    generator = UpdateGenerator();
                }
                return generator;
            }
            set => generator = value;
        }
        private IGenerator generator;

        protected abstract IGenerator UpdateGenerator();

        public IEnumerable<Vector3> Vertices => Generator.Vertices;

        public IEnumerable<int> Triangles => Generator.Triangles;

        public event Action GeneratorUpdated;

        public void OnValidate()
        {
            Generator = UpdateGenerator();
            GeneratorUpdated?.Invoke();
        }
    }
}