using System;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Components.Generators;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Components.Generators
{
    [Category("Unit")]
    public class DiamondSquareTests
    {
        private GameObject gameObject;
        private DiamondSquare ds;
        private DsaSettings settings;

        [SetUp]
        public void Setup()
        {
            settings = new DsaSettings();
            gameObject = new GameObject();
            ds = gameObject.AddComponent<DiamondSquare>();
            var services = ServiceContainerFactory.Create();
            services.AddSingleton(settings);
            services.InjectServicesTo(ds);
        }

        [TearDown]
        public void TearDown()
        {
            UnityEngine.Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void TestResetValues()
        {
            Assert.That(ds.seed, Is.EqualTo(0));
            Assert.That(ds.resolution, Is.EqualTo(5));
            Assert.That(ds.sideLength, Is.EqualTo(1000));
            Assert.That(ds.magnitude, Is.EqualTo(100));
            Assert.That(ds.hardness, Is.EqualTo(0.5f));
        }

        [Test]
        public void TestUpdatedEventInvoked()
        {
            var mockAction = new Mock<Action>();
            ds.Updated += mockAction.Object;
            ds.OnValidate();
            mockAction.Verify(m => m.Invoke(), Times.Once);
        }

        [Test]
        public void TestDSASettingsUpdated()
        {
            const int seed = 10;
            const int resolution = 5;
            const float sideLength = 100;
            const float magnitude = 100;
            const float hardness = 1;

            ds.seed = seed;
            ds.resolution = resolution;
            ds.sideLength = sideLength;
            ds.magnitude = magnitude;
            ds.hardness = hardness;

            var expectedSettings = new DsaSettings
            {
                Seed = seed,
                Resolution = resolution,
                SideLength = sideLength,
                Magnitude = magnitude,
                Hardness = hardness
            };

            ds.OnValidate();

            Assert.That(settings, Is.EqualTo(expectedSettings));
        }
    }
}