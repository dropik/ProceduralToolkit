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
        internal class LandscapeGeneratorWithMockMeshAssembler : LandscapeGenerator
        {
            public Mock<IMeshAssembler> MockMeshAssembler { get; private set; } = new Mock<IMeshAssembler>();
            
            protected override void SetupMeshAssemblerServices(IServiceContainer services)
            {
                services.AddSingleton(MockMeshAssembler.Object);
                services.AddSingleton<DsaSettings>();
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
            var landscapeGenerator = gameObject.AddComponent<LandscapeGeneratorWithMockMeshAssembler>();
            landscapeGenerator.RegisterUndo();
            Undo.PerformUndo();
            Assert.That(gameObject == null);
        }

        [Test]
        public void TestViewInitialized()
        {
            gameObject.AddComponent<LandscapeGeneratorWithMockMeshAssembler>();
            var terrain = gameObject.GetComponent<Terrain>();
            var terrainData = terrain.terrainData;
            Assert.That(terrainData.heightmapResolution, Is.EqualTo(33));
            Assert.That(terrain.materialTemplate, Is.Not.Null);
        }

        [Test]
        public void TestMockAssemblerCalled()
        {
            var landscapeGenerator = gameObject.AddComponent<LandscapeGeneratorWithMockMeshAssembler>();
            var assembler = gameObject.GetComponent<GeneratorStarterComponent>();
            assembler.Start();
            landscapeGenerator.MockMeshAssembler.Verify(m => m.Assemble(), Times.Once);
        }

        [Test]
        public void TestMeshUpdatedOnDsaSettingsChange()
        {
            gameObject.AddComponent<LandscapeGenerator>();
            var assembler = gameObject.GetComponent<GeneratorStarterComponent>();
            assembler.Start();
            var view = gameObject.GetComponent<TerrainGeneratorView>();
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
