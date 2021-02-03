using System;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    public class Plane : BaseGeneratorComponent
    {
        [Min(0)]
        public float length;
        [Min(0)]
        public float width;

        [Service]
        private readonly Func<PlaneGeneratorSettings, IGenerator> planeGeneratorProvider;

        protected override IGenerator UpdateGenerator()
        {
            return planeGeneratorProvider?.Invoke(new PlaneGeneratorSettings()
            {
                Length = length,
                Width = width
            });
        }

        public override void Reset()
        {
            length = 1;
            width = 1;
        }
    }
}