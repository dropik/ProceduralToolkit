using Moq;
using NUnit.Framework;
using ProceduralToolkit.Components.Generators;
using ProceduralToolkit.Services;
using System;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services
{
    [Category("Unit")]
    public class StartupResetterTests
    {
        private GameObject gameObject;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void TestNameSet()
        {
            const string testName = "Test Name";
            var resetter = new StartupResetter(gameObject)
            {
                DefaultName = testName
            };
            resetter.Reset();
            Assert.That(gameObject.name, Is.EqualTo(testName));
        }

        [Test]
        public void TesetGeneratorsReset()
        {
            var mockGenerator1 = new Mock<IGeneratorSettings>();
            var mockGenerator2 = new Mock<IGeneratorSettings>();
            var generators = new IGeneratorSettings[] { mockGenerator1.Object, mockGenerator2.Object };
            var resetter = new StartupResetter(gameObject)
            {
                GeneratorSettings = generators
            };
            resetter.Reset();
            mockGenerator1.Verify(m => m.Reset(), Times.Once);
            mockGenerator2.Verify(m => m.Reset(), Times.Once);
        }

        [Test]
        public void TestInitChildCalled()
        {
            var mockInitChild1 = new Mock<Func<GameObject>>();
            var mockInitChild2 = new Mock<Func<GameObject>>();
            var resetter = new StartupResetter(gameObject);
            resetter.InitChild += mockInitChild1.Object;
            resetter.InitChild += mockInitChild2.Object;
            resetter.Reset();
            mockInitChild1.Verify(m => m.Invoke(), Times.Once);
            mockInitChild2.Verify(m => m.Invoke(), Times.Once);
        }

        [Test]
        public void TestOldHierarchyRemoved()
        {
            var testObject1 = new GameObject();
            var testObject2 = new GameObject();
            var resetter = new StartupResetter(gameObject);
            resetter.InitChild += () => testObject1;
            resetter.InitChild += () => testObject2;
            try
            {
                resetter.Reset();
                resetter.Reset();
                Assert.That(testObject1 == null);
                Assert.That(testObject2 == null);
            }
            finally
            {
                UnityEngine.Object.DestroyImmediate(testObject1);
                UnityEngine.Object.DestroyImmediate(testObject2);
            }
            
        }
    }
}