using System;
using System.Collections;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.DI;
using UnityEngine;
using UnityEngine.TestTools;

namespace ProceduralToolkit.EditorTests.Unit.Services.DI
{
    [Category("Unit")]
    public class ServiceInjectorTests
    {
        const string TEST_MESSAGE = "Test Message";

        [ExecuteInEditMode]
        internal abstract class BaseComponent : MonoBehaviour
        {
            public bool IsTestFinished { get; private set; }

            private void Start()
            {
                IsTestFinished = true;
            }

            public abstract string Message { get; }
        }

        internal class ComponentWithDependency : BaseComponent
        {
            [Service]
            private readonly string message;

            public override string Message => message;
        }

        internal class ComponentWithoutDependency : BaseComponent
        {
            private readonly string message = "";
            
            public override string Message => message;
        }

        internal class ChildComponent : ComponentWithDependency { }
        internal class Example { }

        private Mock<IServiceResolver> mockResolver;
        private ServiceInjector injector;
        private BaseComponent component;
        private Component genericComponent;

        [SetUp]
        public void Setup()
        {
            mockResolver = new Mock<IServiceResolver>();
            mockResolver.Setup(m => m.ResolveService(It.Is<Type>(t => t.Equals(typeof(string)))))
                        .Returns(TEST_MESSAGE);
            injector = new ServiceInjector(mockResolver.Object);
        }

        [TearDown]
        public void TearDown()
        {
            DestroyComponent(component);
            DestroyComponent(genericComponent);
        }

        private void DestroyComponent(Component component)
        {
            if (component != null)
            {
                UnityEngine.Object.DestroyImmediate(component.gameObject);
            }
        }

        [UnityTest]
        public IEnumerator TestDependencySet()
        {
            yield return TestWithComponent<ComponentWithDependency>(TEST_MESSAGE);
        }

        [UnityTest]
        public IEnumerator TestDependencyNotSet()
        {
            yield return TestWithComponent<ComponentWithoutDependency>("");
        }

        [UnityTest]
        public IEnumerator TestDependencySetOnParentClass()
        {
            yield return TestWithComponent<ChildComponent>(TEST_MESSAGE);
        }

        [Test]
        public void TestDependencySetOnDerivedFromComponent()
        {
            genericComponent = new GameObject().AddComponent<BoxCollider>();
            try
            {
                injector.InjectServicesTo(genericComponent);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestDependencySetOnDerivedFromObject()
        {
            var obj = new Example();
            try
            {
                injector.InjectServicesTo(obj);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        private IEnumerator TestWithComponent<TComponent>(string expectedMessage) where TComponent : BaseComponent
        {
            component = new GameObject().AddComponent<TComponent>();
            injector.InjectServicesTo(component);
            while (!component.IsTestFinished)
            {
                yield return null;
            }
            Assert.That(component.Message, Is.EqualTo(expectedMessage));
        }
    }
}