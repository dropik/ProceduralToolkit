using NUnit.Framework;
using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class SquareDsaStepTests : BaseDsaStepTests
    {
        protected override BaseDsaStep CreateDsaStep(LandscapeContext context, IDisplacer displacer)
        {
            return new SquareDsaStep(context, displacer);
        }

        protected override void SetupHeightsForFirstIteration()
        {
            Heights[Mid, Mid] = 18 + Displace();
        }

        protected override float[,] CreateExpectedHeightsForFirstIteration()
        {
            var expectedHeights = Heights.Clone() as float[,];

            expectedHeights[0, Mid] = 13.75f + Displace();
            expectedHeights[Mid, 0] = 25.75f + Displace();
            expectedHeights[Mid, N - 1] = 14.25f + Displace();
            expectedHeights[N - 1, Mid] = 26.25f + Displace();

            return expectedHeights;
        }

        protected override void SetupHeightsForSecondIteration()
        {
            Heights[0, 2] = 1;

            Heights[2, 0] = 1;
            Heights[2, 2] = 1;
            Heights[2, 4] = 1;

            Heights[4, 2] = 1;

            Heights[1, 1] = 2.5f + Displace();
            Heights[1, 3] = 1.75f + Displace();

            Heights[3, 1] = 13.75f + Displace();
            Heights[3, 3] = 3 + Displace();
        }

        protected override float[,] CreateExpectedHeightsForSecondIteration()
        {
            var expectedHeights = Heights.Clone() as float[,];

            expectedHeights[0, 1] = 8.0625f + Displace();
            expectedHeights[0, 3] = 4.4375f + Displace();

            expectedHeights[1, 0] = 5.0625f + Displace();
            expectedHeights[1, 2] = 3.5625f + Displace();
            expectedHeights[1, 4] = 4.3125f + Displace();

            expectedHeights[2, 1] = 6.5625f + Displace();
            expectedHeights[2, 3] = 3.6875f + Displace();

            expectedHeights[3, 0] = 19.4375f + Displace();
            expectedHeights[3, 2] = 6.6875f + Displace();
            expectedHeights[3, 4] = 8.6875f + Displace();

            expectedHeights[4, 1] = 19.3125f + Displace();
            expectedHeights[4, 3] = 5.6875f + Displace();

            return expectedHeights;
        }
    }
}