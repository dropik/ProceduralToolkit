using NUnit.Framework;
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
            Generator.SetActive(false);
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
    }
}
