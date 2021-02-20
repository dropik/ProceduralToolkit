using System;

namespace ProceduralToolkit.Services.Generators
{
    public interface IState
    {
        [Obsolete]
        void MoveNext();
    }
}