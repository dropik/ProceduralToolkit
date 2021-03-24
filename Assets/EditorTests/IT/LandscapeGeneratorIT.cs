using NUnit.Framework;
using ProceduralToolkit.Components;
using ProceduralToolkit.Components.Generators;
using UnityEditor;
using UnityEngine;

namespace ProceduralToolkit.IntegrationTests
{
    [Category("IT")]
    public class LandscapeGeneratorIT
    {
        private GameObject Generator => GameObject.Find("Landscape Generator");

        [SetUp]
        public void Setup()
        {
            MenuEntries.NewLandscapeGenerator();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(Generator);
        }

        [Test]
        public void TestRegisterUndo()
        {
            Undo.PerformUndo();
            Assert.That(Generator == null);
        }

        [Test]
        public void TestViewInitialized()
        {
            var terrain = Generator.GetComponent<Terrain>();
            var terrainData = terrain.terrainData;
            Assert.That(terrainData.heightmapResolution, Is.EqualTo(129));
            Assert.That(terrain.materialTemplate, Is.Not.Null);
        }

        [Test]
        public void TestMeshUpdatedOnDsaSettingsChange()
        {
            var assembler = Generator.GetComponent<GeneratorStarterComponent>();
            assembler.Start();
            var view = Generator.GetComponent<TerrainView>();
            view.Update();
            var terrain = Generator.GetComponent<Terrain>();
            var terrainData = terrain.terrainData;
            var heights1 = terrainData.GetHeights(0, 0, 33, 33);
            var ds = Generator.GetComponent<DiamondSquare>();
            ds.seed = 10;
            ds.OnValidate();
            view.Update();
            var heights2 = terrainData.GetHeights(0, 0, 33, 33);
            CollectionAssert.AreNotEqual(heights1, heights2);
        }
    }
}
