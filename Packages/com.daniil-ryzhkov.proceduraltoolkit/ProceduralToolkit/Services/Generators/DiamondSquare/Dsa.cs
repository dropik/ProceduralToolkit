using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class Dsa : IDsa
    {
        private readonly LandscapeContext context;
        private readonly IAlgorithmStep diamondStep;
        private readonly IAlgorithmStep squareStep;

        public Dsa(LandscapeContext context, IAlgorithmStep diamondStep, IAlgorithmStep squareStep)
        {
            this.context = context;
            this.diamondStep = diamondStep;
            this.squareStep = squareStep;
        }

        public void Execute()
        {
            for (int i = 1; i <= context.Iterations; i++)
            {
                diamondStep.Execute(i);
                squareStep.Execute(i);
            }
        }
    }
}