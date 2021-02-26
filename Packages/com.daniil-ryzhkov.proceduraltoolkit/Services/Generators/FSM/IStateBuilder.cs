using System;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IStateBuilder
    {
        IStateBuilder ConfigureOutput<TOutput>(TOutput output) where TOutput : IStateOutput;
        IStateBuilder ConfigurePreprocessor<TPreprocessor>(TPreprocessor preprocessor) where TPreprocessor : IVertexPreprocessor;
        ITransitionBuilder On(Func<bool> condition);
        IStateBuilder DoNotZeroColumn();
        IStateBuilder SetDefaultState(string name);
        IState BuildState(IMachine machine);
    }
}
