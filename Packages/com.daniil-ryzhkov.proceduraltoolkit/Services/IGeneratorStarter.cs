using System;

namespace ProceduralToolkit.Services
{
    public interface IGeneratorStarter
    {
        event Action Generated;
        void Start();
    }
}