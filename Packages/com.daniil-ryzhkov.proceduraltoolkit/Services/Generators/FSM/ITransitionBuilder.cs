namespace ProceduralToolkit.Services.Generators.FSM
{
    public interface ITransitionBuilder
    {
        IState SetNext(IState next);
    }
}
