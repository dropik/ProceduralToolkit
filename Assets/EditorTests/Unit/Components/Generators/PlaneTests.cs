using System;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Components.Generators
{
    [Category("Unit")]
    public class PlaneTests
    {
        private GameObject obj;
        private ProceduralToolkit.Components.Generators.Plane plane;

        [SetUp]
        public void Setup()
        {
            obj = new GameObject();
            plane = obj.AddComponent<ProceduralToolkit.Components.Generators.Plane>();
        }

        [TearDown]
        public void TearDown()
        {
            if (obj != null)
            {
                UnityEngine.Object.DestroyImmediate(obj);
            }
        }

        [Test]
        public void TestResetValues()
        {
            Assert.That(plane.length, Is.EqualTo(1));
            Assert.That(plane.width, Is.EqualTo(1));
        }

        [Test]
        public void TestLengthMinValue()
        {
            TestMinValueForField("length");
        }

        [Test]
        public void TestWidthMinValue()
        {
            TestMinValueForField("width");
        }

        private void TestMinValueForField(string fieldName)
        {
            var type = plane.GetType();
            var lengthField = type.GetField(fieldName);
            var minAttribute = (MinAttribute)Attribute.GetCustomAttribute(lengthField, typeof(MinAttribute));
            Assert.That(minAttribute.min, Is.Zero);
        }

        [Test]
        public void TestPlaneProviderCalledOnValidate()
        {
            TestWithProviderAndGenerator((mockPlaneProvider, mockGenerator) =>
            {
                plane.length = 2;
                plane.width = 3;

                plane.OnValidate();

                mockPlaneProvider.Verify(
                    m => m.Invoke(It.Is<PlaneGeneratorSettings>(s => s.Length == 2 && s.Width == 3)),
                    Times.Once
                );
            });
        }

        [Test]
        public void TestVerticesRedirectsToProvidedGenerator()
        {
            TestWithProviderAndGenerator((mockPlaneProvider, mockGenerator) =>
            {
                var vertices = plane.Vertices;
                mockGenerator.Verify(m => m.Vertices, Times.Once);
            });
        }

        [Test]
        public void TestTrianglesRedirectsToProvidedGenerator()
        {
            TestWithProviderAndGenerator((mockPlaneProvier, mockGenerator) =>
            {
                var triangles = plane.Triangles;
                mockGenerator.Verify(m => m.Triangles, Times.Once);
            });
        }

        private void TestWithProviderAndGenerator(Action<Mock<Func<PlaneGeneratorSettings, IGenerator>>, Mock<IGenerator>> act)
        {
            var mockGenerator = new Mock<IGenerator>();

            var mockPlaneProvider = new Mock<Func<PlaneGeneratorSettings, IGenerator>>();
            mockPlaneProvider.Setup(m => m.Invoke(It.IsAny<PlaneGeneratorSettings>())).Returns(mockGenerator.Object);

            var services = ServiceContainerFactory.Create();
            services.AddSingleton<Func<PlaneGeneratorSettings, IGenerator>>(mockPlaneProvider.Object);
            services.InjectServicesTo(plane);

            act.Invoke(mockPlaneProvider, mockGenerator);
        }

        [Test]
        public void TestGeneratedEventInvoked()
        {
            var mockEvent = new Mock<Action>();
            plane.GeneratorUpdated += mockEvent.Object;
            plane.OnValidate();
            mockEvent.Verify(m => m.Invoke(), Times.Once);
        }
    }
}