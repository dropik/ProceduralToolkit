using Moq;
using NUnit.Framework;
using ProceduralToolkit.Components;
using ProceduralToolkit.Components.Generators;
using ProceduralToolkit.Components.Startups;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit.IntegrationTests
{
    [Category("IT")]
    public class LandscapeGeneratorIT
    {
        internal class LandscapeGeneratorWithMockGeneratorStarter : LandscapeGenerator
        {
            public Mock<IGeneratorStarter> MockGeneratorStarter { get; private set; } = new Mock<IGeneratorStarter>();
            
            protected override void SetupGeneratorStarterServices(IServiceContainer services)
            {
                services.AddSingleton(MockGeneratorStarter.Object);
                services.AddSingleton<DsaSettings>();
                services.AddSingleton<LandscapeContext>();
            }
        }

        private GameObject gameObject;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void TestRegisterUndo()
        {
            var landscapeGenerator = gameObject.AddComponent<LandscapeGeneratorWithMockGeneratorStarter>();
            landscapeGenerator.RegisterUndo();
            Undo.PerformUndo();
            Assert.That(gameObject == null);
        }

        [Test]
        public void TestViewInitialized()
        {
            gameObject.AddComponent<LandscapeGeneratorWithMockGeneratorStarter>();
            var terrain = gameObject.GetComponent<Terrain>();
            var terrainData = terrain.terrainData;
            Assert.That(terrainData.heightmapResolution, Is.EqualTo(129));
            Assert.That(terrain.materialTemplate, Is.Not.Null);
        }

        [Test]
        public void TestMockAssemblerCalled()
        {
            var landscapeGenerator = gameObject.AddComponent<LandscapeGeneratorWithMockGeneratorStarter>();
            var assembler = gameObject.GetComponent<GeneratorStarterComponent>();
            assembler.Start();
            landscapeGenerator.MockGeneratorStarter.Verify(m => m.Start(), Times.Once);
        }

        [Test]
        public void TestMeshUpdatedOnDsaSettingsChange()
        {
            gameObject.AddComponent<LandscapeGenerator>();
            var assembler = gameObject.GetComponent<GeneratorStarterComponent>();
            assembler.Start();
            var view = gameObject.GetComponent<TerrainView>();
            view.Update();
            var terrain = gameObject.GetComponent<Terrain>();
            var terrainData = terrain.terrainData;
            var heights1 = terrainData.GetHeights(0, 0, 33, 33);
            var ds = gameObject.GetComponent<DiamondSquare>();
            ds.seed = 10;
            ds.OnValidate();
            view.Update();
            var heights2 = terrainData.GetHeights(0, 0, 33, 33);
            CollectionAssert.AreNotEqual(heights1, heights2);
        }
    }
}
