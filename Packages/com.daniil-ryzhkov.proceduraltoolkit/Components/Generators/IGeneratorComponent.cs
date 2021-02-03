using System;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.Components.Generators
{
    public interface IGeneratorComponent : IGenerator
    {
        event Action GeneratorUpdated;

        void Reset();
    }
}