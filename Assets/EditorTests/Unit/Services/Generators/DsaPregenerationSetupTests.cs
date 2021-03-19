using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DsaPregenerationSetupTests
    {
        private DsaSettings settings;
        private LandscapeContext context;
        private Mock<IDsa> mockWrappee;
        private DsaPregenerationSetup setup;

        private void Setup(int iterations)
        {
            mockWrappee = new Mock<IDsa>();
            settings = new DsaSettings
            {
                Resolution = iterations
            };
            context = new LandscapeContext();
            setup = new DsaPregenerationSetup(mockWrappee.Object, settings, context);
        }

        [Test]
        public void TestWrappeeExecuted()
        {
            Setup(2);
            setup.Execute();
            mockWrappee.Verify(m => m.Execute(), Times.Once);
        }

        [Test]
        [TestCase(0, 2)]
        [TestCase(2, 5)]
        public void TestLengthCalculated(int iterations, int expectedLength)
        {
            Setup(iterations);
            setup.Execute();
            Assert.That(context.Length, Is.EqualTo(expectedLength));
        }

        [Test]
        [TestCase(0, 4)]
        [TestCase(2, 25)]
        public void TestVerticesBufferAllocated(int iterations, int expectedLength)
        {
            Setup(iterations);
            setup.Execute();
            Assert.That(context.Vertices.Length, Is.EqualTo(expectedLength));
        }

        [Test]
        public void TestInitialVerticesSet()
        {
            const int iterations = 2;
            const int length = 5;
            Setup(iterations);
            settings.SideLength = 100;
            var expectedUpLeft = new Vector3(-50, 0, 50);
            var expectedUpRight = new Vector3(50, 0, 50);
            var expectedDownLeft = new Vector3(-50, 0, -50);
            var expectedDownRight = new Vector3(50, 0, -50);

            setup.Execute();

            Assert.That(context.Vertices[0], Is.EqualTo(expectedUpLeft));
            Assert.That(context.Vertices[length - 1], Is.EqualTo(expectedUpRight));
            Assert.That(context.Vertices[(length - 1) * length], Is.EqualTo(expectedDownLeft));
            Assert.That(context.Vertices[length * length - 1], Is.EqualTo(expectedDownRight));
        }

        [Test]
        public void TestGridSizeCalculated()
        {
            Setup(2);
            settings.SideLength = 100;
            var expectedGridSize = new Vector3(25, 0, -25);

            setup.Execute();

            Assert.That(context.GridSize, Is.EqualTo(expectedGridSize));
        }
    }
}
