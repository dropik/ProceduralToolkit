using Moq;
using NUnit.Framework;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public abstract class BaseDsaDecoratorTests
    {
        private Mock<IDsa> mockWrappee;
        protected BaseDsaDecorator Decorator { get; private set; }

        [SetUp]
        public void Setup()
        {
            mockWrappee = new Mock<IDsa>();
            PreSetup();
            Decorator = CreateDecorator(mockWrappee.Object);
            PostSetup();
        }

        protected virtual void PreSetup() { }

        protected abstract BaseDsaDecorator CreateDecorator(IDsa wrappee);

        protected virtual void PostSetup() { }

        [Test]
        public void TestWrappeeExecuted()
        {
            Decorator.Execute();
            mockWrappee.Verify(m => m.Execute(), Times.Once);
        }
    }
}