using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.ServiceContainer;
using System;

namespace ProceduralToolkit.EditorTests.Unit.Services.ServiceContainer
{
    [Category("Unit")]
    public class ConstructorServiceFactoryTests
    {
        internal interface IExampleService { }
        internal class ExampleService : IExampleService { }
        internal class WithDependencyService : IExampleService
        {
            public WithDependencyService(IExampleService service) { }
        }

        private Mock<IServiceContainer> mockServiceContainer;

        [SetUp]
        public void Setup()
        {
            mockServiceContainer = new Mock<IServiceContainer>();
        }

        [Test]
        public void TestOnDefaultConstructor()
        {
            ExecuteTest<ExampleService>();
        }

        [Test]
        public void TestOnRegisteredService()
        {
            mockServiceContainer.Setup(m => m.GetService(It.Is<Type>(t => t.Equals(typeof(IExampleService)))))
                                .Returns(new ExampleService());
            ExecuteTest<WithDependencyService>(() =>
            {
                mockServiceContainer.Verify(m => m.GetService(It.Is<Type>(t => t.Equals(typeof(IExampleService)))), Times.Once);
            });
        }

        [Test]
        public void TestOnNotRegisteredService()
        {
            mockServiceContainer.Setup(m => m.GetService(It.Is<Type>(t => t.Equals(typeof(IExampleService)))))
                                .Throws<NotRegisteredServiceException>();
            var factory = new ConstructorServiceFactory<IExampleService, WithDependencyService>(mockServiceContainer.Object);
            try
            {
                factory.CreateService();
                Assert.Fail();
            }
            catch (NotRegisteredServiceException)
            {
                Assert.Pass();
            }
        }

        private void ExecuteTest<TImplementation>()
            where TImplementation : class, IExampleService
        {
            ExecuteTest<TImplementation>(null);
        }

        private void ExecuteTest<TImplementation>(Action testPostProcess)
            where TImplementation : class, IExampleService
        {
            var factory = new ConstructorServiceFactory<IExampleService, TImplementation>(mockServiceContainer.Object);
            var result = factory.CreateService();
            Assert.That(result, Is.InstanceOf<TImplementation>());
            testPostProcess?.Invoke();
        }
    }
}