using NUnit.Framework;
using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class HeightsInitializerTests : BaseDsaDecoratorTests
    {
        private const float BIAS = 0.4f;

        private LandscapeContext context;
        private DsaSettings settings;
        private HeightsInitializer initializer;

        protected override void PreSetup()
        {
            context = new LandscapeContext();
            settings = new DsaSettings
            {
                bias = BIAS
            };
        }

        protected override BaseDsaDecorator CreateDecorator(IDsa wrappee)
            => new HeightsInitializer(wrappee, context, settings);

        protected override void PostSetup()
        {
            initializer = Decorator as HeightsInitializer;
        }

        [Test]
        public void TestHeightsBufferAllocated()
        {
            context.Length = 5;
            initializer.Execute();
            Assert.That(context.Heights.Length, Is.EqualTo(25));
        }

        [Test]
        public void TestCornerHeightsAreSet()
        {
            context.Length = 5;

            initializer.Execute();
            
            Assert.That(context.Heights[0, 0], Is.EqualTo(BIAS));
            Assert.That(context.Heights[0, 4], Is.EqualTo(BIAS));
            Assert.That(context.Heights[4, 0], Is.EqualTo(BIAS));
            Assert.That(context.Heights[4, 4], Is.EqualTo(BIAS));
        }
    }
}
