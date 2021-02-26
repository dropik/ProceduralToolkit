namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface IStateBuilder
    {
        IStateBuilder ConfigureOutput<TOutput>(TOutput output) where TOutput : IStateOutput;
        IStateBuilder ConfigurePreprocessor<TPreprocessor>(TPreprocessor preprocessor) where TPreprocessor : IVertexPreprocessor;
        ITransitionBehaviour Build();
    }
}
