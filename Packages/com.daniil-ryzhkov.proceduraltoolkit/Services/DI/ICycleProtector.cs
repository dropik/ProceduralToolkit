using System;

namespace ProceduralToolkit.Services.DI
{
    public interface ICycleProtector
    {
        void Push(Type type);
    }
}