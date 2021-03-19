using System;

namespace ProceduralToolkit.Components.Generators
{
    public interface IGeneratorSettings
    {
        event Action Updated;
        void Reset();
    }
}