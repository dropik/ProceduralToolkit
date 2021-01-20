using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.ServiceContainer;

namespace ProceduralToolkit.EditorTests.Unit.Services.ServiceContainer
{
    [Category("Unit")]
    public class TransientServiceTests
    {
        public class ExampleClass
        {
            public int Counter { get; set; } = 0;
        }

        private Mock<IServiceFactory<ExampleClass>> mockServiceFactory;

        [Test]
        public void TestTransientService()
        {
            mockServiceFactory = new Mock<IServiceFactory<ExampleClass>>();
            mockServiceFactory.Setup(m => m.CreateService()).Returns(() => new ExampleClass());
            var service = new TransientService<ExampleClass>(mockServiceFactory.Object);

            var instance1 = service.Instance;
            var instance2 = service.Instance;
            instance1.Counter++;

            Assert.That(instance2.Counter, Is.Zero);
        }
    }
}