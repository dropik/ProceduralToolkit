using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
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
        public void TestHeightsBufferAllocated()
        {
            Setup(2);
            setup.Execute();
            Assert.That(context.Heights.Length, Is.EqualTo(25));
        }

        [Test]
        public void TestCornerHeightsAreSet()
        {
            Setup(2);

            setup.Execute();
            
            Assert.That(context.Heights[0, 0], Is.EqualTo(0.5f));
            Assert.That(context.Heights[0, 4], Is.EqualTo(0.5f));
            Assert.That(context.Heights[4, 0], Is.EqualTo(0.5f));
            Assert.That(context.Heights[4, 4], Is.EqualTo(0.5f));
        }
    }
}
