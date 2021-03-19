using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.DI
{
    [Category("Unit")]
    public class ServiceContainerTests
    {
        internal class Example
        {
            public string Name { get; set; }
        }
        internal interface I1 { }
        internal class Class1 : I1 { }

        private Mock<IServiceResolver> mockServiceResolver;
        private Mock<IList<IService>> mockServiceList;
        private Mock<IServiceInjector> mockServiceInjector;
        private Mock<IServiceFactory> mockSingletonFactory;
        private Mock<IServiceFactory> mockTransientFactory;
        private Mock<IService> mockService;
        private ServiceContainer services;

        [SetUp]
        public void Setup()
        {
            mockServiceList = new Mock<IList<IService>>();
            mockServiceResolver = new Mock<IServiceResolver>();
            mockServiceInjector = new Mock<IServiceInjector>();
            mockSingletonFactory = new Mock<IServiceFactory>();
            mockTransientFactory = new Mock<IServiceFactory>();
            services = new ServiceContainer(
                mockServiceList.Object,
                mockServiceResolver.Object,
                mockServiceInjector.Object,
                mockSingletonFactory.Object,
                mockTransientFactory.Object
            );
            mockService = new Mock<IService>();
        }

        [Test]
        public void TestGetService()
        {
            var testObject = SetupResolver();
            var result = services.GetService(testObject.GetType());
            AssertObjectResolved(result, testObject);
        }

        [Test]
        public void TestGetServiceGeneric()
        {
            var testObject = SetupResolver();
            var result = services.GetService<Example>();
            AssertObjectResolved(result, testObject);
        }

        private object SetupResolver()
        {
            var testObject = CreateTestObject();
            mockServiceResolver.Setup(m => m.ResolveService(It.IsAny<Type>())).Returns(testObject);
            return testObject;
        }

        private Example CreateTestObject()
        {
            return new Example()
            {
                Name = "Test Object"
            };
        }

        private void AssertObjectResolved(object result, object expected)
        {
            mockServiceResolver.Verify(m => m.ResolveService(It.IsAny<Type>()), Times.Once);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void TestInjectService()
        {
            var testObject = CreateTestObject();
            services.InjectServicesTo(testObject);
            mockServiceInjector.Verify(
                m => m.InjectServicesTo(It.Is<Example>(obj => obj.Equals(testObject))),
                Times.Once
            );
        }

        [Test]
        public void TestAddSingletonWithImplementation()
        {
            TestAddService<I1, Class1>(mockSingletonFactory, () => services.AddSingleton<I1, Class1>());
        }

        [Test]
        public void TestAddConcreteSingleton()
        {
            TestAddService<Class1, Class1>(mockSingletonFactory, () => services.AddSingleton<Class1>());
        }

        private void TestAddService<T, TImplementation>(Mock<IServiceFactory> mockServiceFactory, Action act)
            where TImplementation : class, T
        {
            mockServiceFactory.Setup(m => m.CreateService<T, TImplementation>()).Returns(mockService.Object);

            act.Invoke();

            mockServiceFactory.Verify(m => m.CreateService<T, TImplementation>(), Times.Once);
            AssertServiceAddedToList();
        }

        [Test]
        public void TestAddSingletonWithFunc()
        {
            Func<I1> func = () => new Class1();
            mockSingletonFactory.Setup(m => m.CreateService<I1>(func)).Returns(mockService.Object);

            services.AddSingleton<I1>(func);

            mockSingletonFactory.Verify(m => m.CreateService<I1>(func), Times.Once);
            AssertServiceAddedToList();
        }

        [Test]
        public void TestAddSingletonWithInstance()
        {
            var obj = new Class1();
            mockSingletonFactory.Setup(m => m.CreateService<I1>(It.Is<Func<I1>>(f => f.Invoke().Equals(obj))))
                                .Returns(mockService.Object);

            services.AddSingleton<I1>(obj);

            mockSingletonFactory.Verify(
                m => m.CreateService<I1>(It.Is<Func<I1>>(f => f.Invoke().Equals(obj))),
                Times.Once
            );
            AssertServiceAddedToList();
        }

        [Test]
        public void TestAddTransientWithImplementation()
        {
            TestAddService<I1, Class1>(mockTransientFactory, () => services.AddTransient<I1, Class1>());
        }

        [Test]
        public void TestAddConcreteTransient()
        {
            TestAddService<Class1, Class1>(mockTransientFactory, () => services.AddTransient<Class1>());
        }

        [Test]
        public void TestAddTransientWithFunc()
        {
            Func<I1> func = () => new Class1();
            mockTransientFactory.Setup(m => m.CreateService<I1>(func)).Returns(mockService.Object);

            services.AddTransient<I1>(func);

            mockTransientFactory.Verify(m => m.CreateService<I1>(func), Times.Once);
            AssertServiceAddedToList();
        }

        private void AssertServiceAddedToList()
        {
            mockServiceList.Verify(
                m => m.Add(It.Is<IService>(s => s.Equals(mockService.Object))),
                Times.Once
            );
        }
    }
}
