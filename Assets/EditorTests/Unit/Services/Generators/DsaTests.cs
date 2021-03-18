using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DsaTests
    {
        private LandscapeContext context;

        private void SetupContext(int iterations)
        {
            context = new LandscapeContext { Iterations = iterations };
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void TestStepsExecuted(int iterations)
        {
            SetupContext(iterations);
            var mockDiamondStep = new Mock<IDsaStep>();
            var mockSquareStep = new Mock<IDsaStep>();
            var dsa = new Dsa(context, mockDiamondStep.Object, mockSquareStep.Object);

            dsa.Execute();

            for (int i = 1; i <= iterations; i++)
            {
                mockDiamondStep.Verify(m => m.Execute(i), Times.Once);
                mockSquareStep.Verify(m => m.Execute(i), Times.Once);
            }
        }

        [Test]
        [TestCase(0, 2)]
        [TestCase(2, 5)]
        public void TestLengthCalculated(int iterations, int expectedLength)
        {
            SetupContext(iterations);
            var dsa = new Dsa(context, null, null);

            dsa.Execute();

            Assert.That(context.Length, Is.EqualTo(expectedLength));
        }

        [Test]
        [TestCase(0, 4)]
        [TestCase(2, 25)]
        public void TestVerticesBufferAllocated(int iterations, int expectedLength)
        {
            SetupContext(iterations);
            var dsa = new Dsa(context, null, null);

            dsa.Execute();

            Assert.That(context.Vertices.Length, Is.EqualTo(expectedLength));
        }
    }
}