using Moq;
using NUnit.Framework;
using System;

namespace ProceduralToolkit.Services.DI
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

        private Mock<IServiceResolver> mockServiceResolver;

        [SetUp]
        public void Setup()
        {
            mockServiceResolver = new Mock<IServiceResolver>();
        }

        [Test]
        public void TestOnDefaultConstructor()
        {
            ExecuteTest<ExampleService>();
        }

        [Test]
        public void TestOnNotRegisteredService()
        {
            mockServiceResolver.Setup(m => m.ResolveService(It.Is<Type>(t => t.Equals(typeof(IExampleService)))))
                                .Throws<NotRegisteredServiceException>();
            var factory = new ConstructorServiceFactory<IExampleService, WithDependencyService>(mockServiceResolver.Object);
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

        [Test]
        public void TestOnRegisteredService()
        {
            mockServiceResolver.Setup(m => m.ResolveService(It.Is<Type>(t => t.Equals(typeof(IExampleService)))))
                               .Returns(new ExampleService());
            var factory = new ConstructorServiceFactory<IExampleService, WithDependencyService>(mockServiceResolver.Object);
            var result = factory.CreateService();
            Assert.That(result, Is.InstanceOf<WithDependencyService>());
        }

        [Test]
        public void TestRegisterConcreteService()
        {
            ExecuteTest<ExampleService, ExampleService>();
        }

        private void ExecuteTest<TImplementation>()
            where TImplementation : class, IExampleService
        {
            ExecuteTest<TImplementation>(null);
        }

        private void ExecuteTest<T, TImplementation>()
            where TImplementation : class, T
        {
            ExecuteTest<T, TImplementation>(null);
        }

        private void ExecuteTest<TImplementation>(Action testPostProcess)
            where TImplementation : class, IExampleService
        {
            ExecuteTest<IExampleService, TImplementation>(testPostProcess);
        }

        private void ExecuteTest<T, TImplementation>(Action testPostProcess)
            where TImplementation : class, T
        {
            var factory = new ConstructorServiceFactory<T, TImplementation>(mockServiceResolver.Object);
            var result = factory.CreateService();
            Assert.That(result, Is.InstanceOf<TImplementation>());
            testPostProcess?.Invoke();
        }
    }
}