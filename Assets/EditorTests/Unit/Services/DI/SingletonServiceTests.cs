using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.DI;

namespace ProceduralToolkit.EditorTests.Unit.Services.DI
{
    [Category("Unit")]
    public class SingletonServiceTests
    {
        [Test]
        public void TestSingletonService()
        {
            var mockServiceFactory = new Mock<IServiceFactory<ExampleClass>>();
            mockServiceFactory.Setup(m => m.CreateService()).Returns(() => new ExampleClass());
            var service = new SingletonService<ExampleClass>(mockServiceFactory.Object);
            
            var instance1 = service.Instance;
            var instance2 = service.Instance;
            instance1.Counter++;
            
            Assert.That(instance2.Counter, Is.EqualTo(1));
            Assert.That(service.InstanceType, Is.EqualTo(typeof(ExampleClass)));
        }
    }
}