namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface ITransitionBuilder
    {
        ITransitionBehaviour SetNext(ITransitionBehaviour next);
    }
}
