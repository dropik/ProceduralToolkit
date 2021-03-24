using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [Category("Unit")]
    public class TerrainViewTests
    {
        private readonly float[,] heights = new float[RESOLUTION, RESOLUTION];

        private GameObject gameObject;
        private TerrainData terrainData;
        private LandscapeContext context;
        private TerrainView view;

        private const int RESOLUTION = 33;

        [SetUp]
        public void Setup()
        {
            gameObject = new GameObject();

            terrainData = new TerrainData();
            terrainData.heightmapResolution = RESOLUTION;
            terrainData.SetHeights(0, 0, heights);

            context = new LandscapeContext
            {
                Heights = heights
            };

            view = gameObject.AddComponent<TerrainView>();
            var services = ServiceContainerFactory.Create();
            services.AddSingleton(terrainData);
            services.AddSingleton(context);
            services.InjectServicesTo(view);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(gameObject);
        }

        [Test]
        public void TestNothingHappensOnNonDirty()
        {
            view.Update();
            CollectionAssert.AreEqual(heights, terrainData.GetHeights(0, 0, RESOLUTION, RESOLUTION));
        }

        [Test]
        public void TestTerrainHeightsUpdatedOnMarkedDirty()
        {
            var heights = new float[RESOLUTION, RESOLUTION];
            heights[0, 3] = 0.8f;
            heights[0, 10] = 0.54f;
            heights[1, 17] = 0.3f;
            heights[3, 24] = 1;

            context.Heights = heights;

            view.MarkDirty();
            view.Update();

            CollectionAssert.AreEqual(heights,
                                      terrainData.GetHeights(0, 0, RESOLUTION, RESOLUTION),
                                      Comparer<float>.Create((x, y) => Mathf.Abs(x - y) >= 0.0001f ? 1 : 0));
        }

        [Test]
        public void TestDirtyFlagRemovedAfterUpdate()
        {
            view.MarkDirty();
            view.Update();
            Assert.That(view.IsDirty, Is.False);
        }
    }
}
