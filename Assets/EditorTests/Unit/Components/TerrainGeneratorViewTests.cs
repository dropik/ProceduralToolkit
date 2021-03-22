using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [Category("Unit")]
    public class TerrainGeneratorViewTests
    {
        private readonly float[,] heights = new float[RESOLUTION, RESOLUTION];

        private GameObject gameObject;
        private Terrain terrain;
        private TerrainData terrainData;
        private TerrainGeneratorView view;

        private const int RESOLUTION = 33;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();

            terrain = gameObject.AddComponent<Terrain>();
            terrainData = new TerrainData();
            terrain.terrainData = terrainData;
            terrainData.heightmapResolution = RESOLUTION;
            terrainData.SetHeights(0, 0, heights);

            view = gameObject.AddComponent<TerrainGeneratorView>();
            var services = ServiceContainerFactory.Create();
            services.AddSingleton(terrain);
            services.AddSingleton(new DsaSettings { Magnitude = 300 });
            services.InjectServicesTo(view);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void TestNothingHappensOnNullNewContext()
        {
            view.Update();
            CollectionAssert.AreEqual(heights, terrainData.GetHeights(0, 0, RESOLUTION, RESOLUTION));
        }

        [Test]
        public void TestTerrainHeightsUpdatedOnNewContext()
        {
            var heights = new float[RESOLUTION, RESOLUTION];
            heights[0, 3] = 0.8f;
            heights[0, 10] = 0.54f;
            heights[1, 17] = 0.3f;
            heights[3, 24] = 1;

            var context = new LandscapeContext
            {
                Heights = heights
            };

            view.NewContext = context;
            view.Update();

            CollectionAssert.AreEqual(heights,
                                      terrainData.GetHeights(0, 0, RESOLUTION, RESOLUTION),
                                      Comparer<float>.Create((x, y) => Mathf.Abs(x - y) >= 0.0001f ? 1 : 0));
        }

        [Test]
        public void TestNewContextNulledAfterUpdate()
        {
            view.NewContext = new LandscapeContext
            {
                Heights = new float[RESOLUTION, RESOLUTION]
            };
            view.Update();
            Assert.That(view.NewContext, Is.Null);
        }
    }
}
