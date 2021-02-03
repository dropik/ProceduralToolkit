using System;
using System.Collections.Generic;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    public class Plane : MonoBehaviour, IGenerator
    {
        [Min(0)]
        public float length;
        [Min(0)]
        public float width;

        [Service]
        private readonly Func<PlaneGeneratorSettings, IGenerator> planeGeneratorProvider;

        private IGenerator Generator
        {
            get
            {
                if (generator == null)
                {
                    UpdateGenerator();
                }
                return generator;
            }
            set => generator = value;
        }
        private IGenerator generator;

        private void UpdateGenerator()
        {
            Generator = planeGeneratorProvider?.Invoke(new PlaneGeneratorSettings()
            {
                Length = length,
                Width = width
            });
        }

        public IEnumerable<Vector3> Vertices => Generator.Vertices;

        public IEnumerable<int> Triangles => Generator.Triangles;

        public void Reset()
        {
            length = 1;
            width = 1;
        }

        public void OnValidate()
        {
            UpdateGenerator();
        }
    }
}