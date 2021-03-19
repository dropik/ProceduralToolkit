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
        public void TestNothingHappensOnNullNewMesh()
        {
            view.Update();
            CollectionAssert.AreEqual(heights, terrainData.GetHeights(0, 0, RESOLUTION, RESOLUTION));
        }

        [Test]
        public void TestTerrainHeightsUpdatedOnNewMesh()
        {
            var vertices = new Vector3[RESOLUTION * RESOLUTION];
            vertices[3] = new Vector3(0, 200, 0);
            vertices[10] = new Vector3(0, 24, 0);
            vertices[50] = new Vector3(0, -100, 0);
            vertices[123] = new Vector3(0, 300, 0);

            var mesh = new Mesh()
            {
                vertices = vertices
            };

            var expectedHeights = new float[RESOLUTION, RESOLUTION];
            for (int i = 0; i < RESOLUTION; i++)
            {
                for (int j = 0; j < RESOLUTION; j++)
                {
                    expectedHeights[i, j] = 0.5f;
                }
            }
            expectedHeights[0, 3] = 0.8333333333f;
            expectedHeights[0, 10] = 0.54f;
            expectedHeights[1, 17] = 0.3333333333f;
            expectedHeights[3, 24] = 1;

            view.NewMesh = mesh;
            view.Update();

            CollectionAssert.AreEqual(expectedHeights,
                                      terrainData.GetHeights(0, 0, RESOLUTION, RESOLUTION),
                                      Comparer<float>.Create((x, y) => Mathf.Abs(x - y) >= 0.0001f ? 1 : 0));
        }

        [Test]
        public void TestNewMeshNulledAfterUpdate()
        {
            view.NewMesh = new Mesh()
            {
                vertices = new Vector3[RESOLUTION * RESOLUTION]
            };
            view.Update();
            Assert.That(view.NewMesh, Is.Null);
        }
    }
}
