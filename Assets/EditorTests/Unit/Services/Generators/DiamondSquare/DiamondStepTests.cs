using NUnit.Framework;
using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class DiamondStepTests : BaseAlgorithmStepTests
    {
        protected override BaseAlgorithmStep CreateDsaStep(LandscapeContext context, IDisplacer displacer)
        {
            return new DiamondStep(context, displacer);
        }

        protected override void SetupHeightsForFirstIteration() { }

        protected override float[,] CreateExpectedHeightsForFirstIteration()
        {
            var expectedHeights = Heights.Clone() as float[,];
            expectedHeights[Mid, Mid] = 18 + Displace();
            return expectedHeights;
        }

        protected override void SetupHeightsForSecondIteration()
        {
            Heights[0, 2] = 1;

            Heights[2, 0] = 1;
            Heights[2, 2] = 1;
            Heights[2, 4] = 1;

            Heights[4, 2] = 1;
        }

        protected override float[,] CreateExpectedHeightsForSecondIteration()
        {
            var expectedHeights = Heights.Clone() as float[,];

            expectedHeights[1, 1] = 2.5f + Displace();
            expectedHeights[1, 3] = 1.75f + Displace();

            expectedHeights[3, 1] = 13.75f + Displace();
            expectedHeights[3, 3] = 3 + Displace();

            return expectedHeights;
        }
    }
}