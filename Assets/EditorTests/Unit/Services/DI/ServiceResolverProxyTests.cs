using System;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.DI;

namespace ProceduralToolkit.EditorTests.Unit.Services.DI
{
    [Category("Unit")]
    public class ServiceResolverProxyTests
    {
        private Mock<IServiceResolver> mockResolver;
        private Mock<Func<IServiceResolver>> mockResolverProvider;
        private ServiceResolverProxy proxy;

        [SetUp]
        public void Setup()
        {
            mockResolver = new Mock<IServiceResolver>();
            mockResolverProvider = new Mock<Func<IServiceResolver>>();
            mockResolverProvider.Setup(m => m.Invoke()).Returns(() => mockResolver.Object);
            proxy = new ServiceResolverProxy(mockResolverProvider.Object);
        }

        [Test]
        public void TestResolverProviderInvoked()
        {
            proxy.ResolveService(typeof(object));
            mockResolverProvider.Verify(m => m.Invoke(), Times.Once);
        }

        [Test]
        public void TestProvidedObjectUsedToResolveService()
        {
            proxy.ResolveService(typeof(object));
            mockResolver.Verify(
                m => m.ResolveService(It.Is<Type>(t => t.Equals(typeof(object)))),
                Times.Once
            );
        }
    }
}