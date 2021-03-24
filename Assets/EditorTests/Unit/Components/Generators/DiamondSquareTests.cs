using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using System;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    [Category("Unit")]
    public class DiamondSquareTests
    {
        private GameObject gameObject;
        private DsaSettings settings;
        private Mock<Action> mockOnUpdatedAction;
        private DiamondSquare ds;

        [SetUp]
        public void Setup()
        {
            settings = new DsaSettings();
            mockOnUpdatedAction = new Mock<Action>();

            gameObject = new GameObject();
            ds = gameObject.AddComponent<DiamondSquare>();
            var services = ServiceContainerFactory.Create();
            services.AddSingleton(settings);
            services.InjectServicesTo(ds);
            ds.Updated += mockOnUpdatedAction.Object;
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
            Assert.That(ds.magnitude, Is.EqualTo(1));
            Assert.That(ds.hardness, Is.EqualTo(1));
            Assert.That(ds.bias, Is.EqualTo(0.5f));
        }

        [Test]
        public void TestUpdatedEventInvoked()
        {
            ds.OnValidate();
            mockOnUpdatedAction.Verify(m => m.Invoke(), Times.Once);
        }

        [Test]
        public void TestDSASettingsUpdated()
        {
            const int seed = 10;
            const float magnitude = 0.5f;
            const float hardness = 1;
            const float bias = 0;

            ds.seed = seed;
            ds.magnitude = magnitude;
            ds.hardness = hardness;
            ds.bias = bias;

            var expectedSettings = new DsaSettings
            {
                Seed = seed,
                Magnitude = magnitude,
                Hardness = hardness,
                Bias = bias
            };

            ds.OnValidate();

            Assert.That(settings, Is.EqualTo(expectedSettings));
        }

        [Test]
        public void TestUpdatedEvent_NotInvoked_OnTerrainChanged_WithoutHeightmapResolutionFlag()
        {
            ds.OnTerrainChanged(TerrainChangedFlags.DelayedHeightmapUpdate);
            mockOnUpdatedAction.Verify(m => m.Invoke(), Times.Never);
        }

        [Test]
        public void TestUpdatedEvent_Invoked_OnTerrainChanged_WithHeightmapResolutionFlag()
        {
            ds.OnTerrainChanged(TerrainChangedFlags.DelayedHeightmapUpdate | TerrainChangedFlags.HeightmapResolution);
            mockOnUpdatedAction.Verify(m => m.Invoke(), Times.Once);
        }
    }
}