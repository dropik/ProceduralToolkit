using System;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IStateFactory
    {
        ITransitionBehaviour CreateState();
        IStateFactory ConfigureBuilder(Action<IStateBuilder> build);
    }
}
