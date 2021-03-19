using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class Dsa : IDsa
    {
        private readonly LandscapeContext context;
        private readonly IDsaStep diamondStep;
        private readonly IDsaStep squareStep;

        public Dsa(LandscapeContext context, IDsaStep diamondStep, IDsaStep squareStep)
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