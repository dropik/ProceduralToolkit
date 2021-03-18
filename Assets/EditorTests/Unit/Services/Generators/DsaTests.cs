using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DsaTests
    {
        private LandscapeContext context;
        private Mock<IDsaStep> mockDiamondStep;
        private Mock<IDsaStep> mockSquareStep;
        private Dsa dsa;

        private void SetupContext(int iterations)
        {
            context = new LandscapeContext { Iterations = iterations };
            mockDiamondStep = new Mock<IDsaStep>();
            mockSquareStep = new Mock<IDsaStep>();
            dsa = new Dsa(context, mockDiamondStep.Object, mockSquareStep.Object);
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void TestStepsExecuted(int iterations)
        {
            SetupContext(iterations);

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
            dsa.Execute();
            Assert.That(context.Length, Is.EqualTo(expectedLength));
        }

        [Test]
        [TestCase(0, 4)]
        [TestCase(2, 25)]
        public void TestVerticesBufferAllocated(int iterations, int expectedLength)
        {
            SetupContext(iterations);
            dsa.Execute();
            Assert.That(context.Vertices.Length, Is.EqualTo(expectedLength));
        }

        [Test]
        public void TestInitialVerticesSet()
        {
            const int iterations = 2;
            const int length = 5;
            SetupContext(iterations);
            context.SideLength = 100;
            var expectedUpLeft = new Vector3(-50, 0, 50);
            var expectedUpRight = new Vector3(50, 0, 50);
            var expectedDownLeft = new Vector3(-50, 0, -50);
            var expectedDownRight = new Vector3(50, 0, -50);

            dsa.Execute();

            Assert.That(context.Vertices[0], Is.EqualTo(expectedUpLeft));
            Assert.That(context.Vertices[length - 1], Is.EqualTo(expectedUpRight));
            Assert.That(context.Vertices[(length - 1) * length], Is.EqualTo(expectedDownLeft));
            Assert.That(context.Vertices[length * length - 1], Is.EqualTo(expectedDownRight));
        }

        [Test]
        public void TestGridSizeCalculated()
        {
            SetupContext(2);
            context.SideLength = 100;
            var expectedGridSize = new Vector3(25, 0, -25);

            dsa.Execute();

            Assert.That(context.GridSize, Is.EqualTo(expectedGridSize));
        }
    }
}