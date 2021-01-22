using System;
using System.Collections.Generic;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.DI;

namespace ProceduralToolkit.EditorTests.Unit.Services.DI
{
    [Category("Unit")]
    public class ServiceResolverTests
    {
        public interface I1 { }
        public abstract class BaseClass1 : I1 { }
        public class Class1 : BaseClass1 { }
        public class Class2 : Class1 { }
        public interface IGeneric<T> { }
        public class ClassGeneric : IGeneric<int> { }

        private Mock<IList<IService>> mockList;
        private ServiceResolver resolver;

        [SetUp]
        public void Setup()
        {
            mockList = new Mock<IList<IService>>();
            resolver = new ServiceResolver(mockList.Object);
        }

        [Test]
        public void TestResolveServiceOnConcreteClass()
        {
            TestResolveService<Class1, Class1>(() => new Class1());
        }

        [Test]
        public void TestResolveServiceOnInterface()
        {
            TestResolveService<I1, Class1>(() => new Class1());
        }

        [Test]
        public void TestResolveServiceOnBaseClass()
        {
            TestResolveService<BaseClass1, Class1>(() => new Class1());
        }

        [Test]
        public void TestResolveServiceOnTwoInheritances()
        {
            TestResolveService<BaseClass1, Class2>(() => new Class2());
        }

        [Test]
        public void TestResolveServiceOnGenerics()
        {
            TestResolveService<IGeneric<int>, ClassGeneric>(() => new ClassGeneric());
        }

        [Test]
        public void TestExceptionThrownOnNotFoundService()
        {
            mockList.Setup(m => m.GetEnumerator()).Returns(new Mock<IEnumerator<IService>>().Object);
            try
            {
                resolver.ResolveService(typeof(I1));
                Assert.Fail();
            }
            catch (NotRegisteredServiceException)
            {
                Assert.Pass();
            }
        }

        private void TestResolveService<T, TExpected>(Func<T> provider)
        {
            mockList.Setup(m => m.GetEnumerator()).Returns(GetService(provider));
            
            var result = resolver.ResolveService(typeof(T));
            Assert.That(result, Is.InstanceOf<TExpected>());
        }

        private IEnumerator<IService> GetService<T>(Func<T> provider)
        {
            var mockService = new Mock<IService>();
            mockService.Setup(m => m.Instance).Returns(provider.Invoke());
            yield return mockService.Object;
        }
    }
}