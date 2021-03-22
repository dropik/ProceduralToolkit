using NUnit.Framework;
using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class HeightsInitializerTests : BaseDsaDecoratorTests
    {
        private LandscapeContext context;
        private HeightsInitializer initializer;

        protected override void PreSetup()
        {
            context = new LandscapeContext();
        }

        protected override BaseDsaDecorator CreateDecorator(IDsa wrappee)
            => new HeightsInitializer(wrappee, context);

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
            
            Assert.That(context.Heights[0, 0], Is.EqualTo(0.5f));
            Assert.That(context.Heights[0, 4], Is.EqualTo(0.5f));
            Assert.That(context.Heights[4, 0], Is.EqualTo(0.5f));
            Assert.That(context.Heights[4, 4], Is.EqualTo(0.5f));
        }
    }
}
