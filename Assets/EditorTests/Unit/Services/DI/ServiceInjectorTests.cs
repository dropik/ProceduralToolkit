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

        private Mock<IServiceResolver> mockResolver;
        private ServiceInjector injector;
        private BaseComponent component;

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