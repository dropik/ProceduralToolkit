using Moq;
using NUnit.Framework;
using System;

namespace ProceduralToolkit.Services.DI
{
    [Category("Unit")]
    public class CycleProtectorTests
    {
        internal interface I { }
        internal class Class1 : I { }
        internal class Class2 : I { }

        private Mock<IServiceResolver> mockResolver;
        private CycleProtector protector;

        [SetUp]
        public void Setup()
        {
            mockResolver = new Mock<IServiceResolver>();
            protector = new CycleProtector(mockResolver.Object);
        }

        [Test]
        public void TestBaseCalled()
        {
            protector.ResolveService(typeof(Class1));
            mockResolver.Verify(m => m.ResolveService(It.Is<Type>(t => t.Equals(typeof(Class1)))), Times.Once);
        }

        [Test]
        public void TestPushDoesNotThrowExceptionOnDifferentClasses()
        {
            protector.ResolveService(typeof(Class1));
            protector.ResolveService(typeof(Class2));
            Assert.Pass();
        }

        [Test]
        public void TestPushThrowsExceptionOnSameClasses()
        {
            TestPush(() =>
            {
                protector.ResolveService(typeof(Class1));
                protector.ResolveService(typeof(Class1));
            });
        }

        [Test]
        public void TestPushThrowsExceptionOnPushingInterfaceFirst()
        {
            TestPush(() =>
            {
                protector.ResolveService(typeof(I));
                protector.ResolveService(typeof(Class1));
            });
        }

        [Test]
        public void TestPushThrowsExceptionOnPushingConcreteFirst()
        {
            TestPush(() =>
            {
                protector.ResolveService(typeof(Class1));
                protector.ResolveService(typeof(I));
            });
        }

        private void TestPush(Action pushScenario)
        {
            try
            {
                pushScenario.Invoke();
                Assert.Fail();
            }
            catch (CircularDependencyException)
            {
                Assert.Pass();
            }
        }
    }
}