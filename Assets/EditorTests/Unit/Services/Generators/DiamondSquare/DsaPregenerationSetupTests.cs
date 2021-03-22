using NUnit.Framework;
using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class DsaPregenerationSetupTests : BaseDsaDecoratorTests
    {
        private DsaSettings settings;
        private LandscapeContext context;
        private DsaPregenerationSetup setup;

        protected override void PreSetup()
        {
            settings = new DsaSettings();
            context = new LandscapeContext();
        }

        protected override BaseDsaDecorator CreateDecorator(IDsa wrappee)
            => new DsaPregenerationSetup(wrappee, settings, context);

        protected override void PostSetup()
        {
            setup = Decorator as DsaPregenerationSetup;
        }

        [Test]
        [TestCase(0, 2)]
        [TestCase(2, 5)]
        public void TestLengthCalculated(int iterations, int expectedLength)
        {
            settings.Resolution = iterations;
            setup.Execute();
            Assert.That(context.Length, Is.EqualTo(expectedLength));
        }

        [Test]
        public void TestHeightsBufferAllocated()
        {
            settings.Resolution = 2;
            setup.Execute();
            Assert.That(context.Heights.Length, Is.EqualTo(25));
        }

        [Test]
        public void TestCornerHeightsAreSet()
        {
            settings.Resolution = 2;

            setup.Execute();
            
            Assert.That(context.Heights[0, 0], Is.EqualTo(0.5f));
            Assert.That(context.Heights[0, 4], Is.EqualTo(0.5f));
            Assert.That(context.Heights[4, 0], Is.EqualTo(0.5f));
            Assert.That(context.Heights[4, 4], Is.EqualTo(0.5f));
        }
    }
}
