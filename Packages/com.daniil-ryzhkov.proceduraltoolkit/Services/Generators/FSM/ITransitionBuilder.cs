namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface ITransitionBuilder
    {
        IStateBuilder SetNext(string name);
    }
}
