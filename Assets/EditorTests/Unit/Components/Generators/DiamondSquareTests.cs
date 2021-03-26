using Moq;
using NUnit.Framework;
using System;
using UnityEngine;

namespace ProceduralToolkit.Components.Generators
{
    [Category("Unit")]
    public class DiamondSquareTests
    {
        private GameObject gameObject;
        private Mock<Action> mockOnUpdatedAction;
        private DiamondSquare ds;

        [SetUp]
        public void Setup()
        {
            mockOnUpdatedAction = new Mock<Action>();

            gameObject = new GameObject();
            ds = gameObject.AddComponent<DiamondSquare>();
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
            Assert.That(ds.settings.seed, Is.EqualTo(0));
            Assert.That(ds.settings.magnitude, Is.EqualTo(1));
            Assert.That(ds.settings.hardness, Is.EqualTo(1));
            Assert.That(ds.settings.bias, Is.EqualTo(0.5f));
        }

        [Test]
        public void TestUpdatedEventInvoked()
        {
            ds.OnValidate();
            mockOnUpdatedAction.Verify(m => m.Invoke(), Times.Once);
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