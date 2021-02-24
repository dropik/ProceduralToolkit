using System;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IState : IStateBehaviour
    {
        IState SetDefaultNext(IState next);

        ITransitionBuilder On(Func<bool> condition);
    }
}
