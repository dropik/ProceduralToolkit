using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class DsaTests
    {
        private LandscapeContext context;
        private Mock<IAlgorithmStep> mockDiamondStep;
        private Mock<IAlgorithmStep> mockSquareStep;
        private Dsa dsa;

        private void Setup(int iterations)
        {
            context = new LandscapeContext()
            {
                Iterations = iterations
            };
            mockDiamondStep = new Mock<IAlgorithmStep>();
            mockSquareStep = new Mock<IAlgorithmStep>();
            dsa = new Dsa(context, mockDiamondStep.Object, mockSquareStep.Object);
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void TestStepsExecuted(int iterations)
        {
            Setup(iterations);

            dsa.Execute();

            for (int i = 1; i <= iterations; i++)
            {
                mockDiamondStep.Verify(m => m.Execute(i), Times.Once);
                mockSquareStep.Verify(m => m.Execute(i), Times.Once);
            }
        }
    }
}