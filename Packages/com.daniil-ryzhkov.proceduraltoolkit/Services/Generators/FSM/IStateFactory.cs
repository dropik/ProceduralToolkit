using System;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IStateFactory
    {
        IState CreateState();
        IStateFactory ConfigureBuilder(Action<IStateBuilder> build);
    }
}
