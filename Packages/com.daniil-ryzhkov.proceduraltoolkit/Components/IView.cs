namespace ProceduralToolkit.Components
{
    public interface IView
    {
        void MarkDirty();
        bool IsDirty { get; }
    }
}
