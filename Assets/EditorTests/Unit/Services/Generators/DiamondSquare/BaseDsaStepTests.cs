using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public abstract class BaseDsaStepTests
    {
        private const int DISPLACEMENT = 4;
        protected int N { get; set; } = 5;
        protected int Mid => (N - 1) / 2;

        protected float[,] Heights { get; private set; }
        private Mock<IDisplacer> mockDisplacer;
        protected BaseDsaStep Step { get; private set; }

        protected float Displace() => mockDisplacer.Object.GetNormalizedDisplacement(2);

        [SetUp]
        public void Setup()
        {
            Heights = new float[N, N];
            Heights[0, 0] = 7;
            Heights[0, N - 1] = 4;
            Heights[N - 1, 0] = 52;
            Heights[N - 1, N - 1] = 9;

            mockDisplacer = new Mock<IDisplacer>();
            mockDisplacer.Setup(m => m.GetNormalizedDisplacement(It.IsAny<int>())).Returns(DISPLACEMENT);

            var context = new LandscapeContext
            {
                Heights = Heights,
                Length = N
            };
            Step = CreateDsaStep(context, mockDisplacer.Object);
        }

        protected abstract BaseDsaStep CreateDsaStep(LandscapeContext context, IDisplacer displacer);

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        public void TestIteration(int iteration)
        {
            SetupHeightsForIteration(iteration);
            var expectedHeights = CreateExpectedHeightsForIteration(iteration);

            Step.Execute(iteration);

            CollectionAssert.AreEqual(expectedHeights, Heights);
        }

        private void SetupHeightsForIteration(int iteration)
        {
            if (iteration == 1) SetupHeightsForFirstIteration();
            else if (iteration == 2) SetupHeightsForSecondIteration();
        }

        private float[,] CreateExpectedHeightsForIteration(int iteration)
        {
            if (iteration == 1) return CreateExpectedHeightsForFirstIteration();
            else if (iteration == 2) return CreateExpectedHeightsForSecondIteration();
            return default;
        }

        protected abstract void SetupHeightsForFirstIteration();
        protected abstract float[,] CreateExpectedHeightsForFirstIteration();

        protected abstract void SetupHeightsForSecondIteration();
        protected abstract float[,] CreateExpectedHeightsForSecondIteration();
    }
}