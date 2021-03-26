using System.Collections.Generic;
using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class HeightsSaverTests : BaseDsaDecoratorTests
    {
        private const int RESOLUTION = 33;

        private TerrainData terrainData;
        private LandscapeContext context;

        protected override void PreSetup()
        {
            terrainData = new TerrainData { heightmapResolution = RESOLUTION };
            context = new LandscapeContext();
        }

        protected override BaseDsaDecorator CreateDecorator(IDsa wrappee)
            => new HeightsSaver(wrappee, terrainData, context);

        [Test]
        public void TestHeightsAreStored()
        {
            var expectedHeights = new float[33, 33];
            expectedHeights[3, 0] = 0.5f;
            expectedHeights[5, 7] = 0.33f;
            expectedHeights[30, 23] = 0.78f;
            context.Heights = expectedHeights;

            Decorator.Execute();

            CollectionAssert.AreEqual(expectedHeights,
                                      terrainData.GetHeights(0, 0, RESOLUTION, RESOLUTION),
                                      Comparer<float>.Create((x, y) => Mathf.Abs(x - y) >= 0.0001f ? 1 : 0));
        }
    }
}