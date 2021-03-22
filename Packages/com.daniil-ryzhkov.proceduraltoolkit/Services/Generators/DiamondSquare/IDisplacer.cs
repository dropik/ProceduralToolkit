namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public interface IDisplacer
    {
        float GetDisplacement(int iteration);
        float GetNormalizedDisplacement(int iteration);
    }
}
