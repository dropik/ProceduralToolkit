using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class TerrainDataToContextConverterTests : BaseDsaDecoratorTests
    {
        private const int RESOLUTION = 33;
        private const int ITERATIONS = 5;

        private TerrainData terrainData;
        private LandscapeContext context;
        private TerrainDataToContextConverter converter;

        protected override void PreSetup()
        {
            terrainData = new TerrainData()
            {
                heightmapResolution = RESOLUTION
            };
            context = new LandscapeContext();
        }

        protected override BaseDsaDecorator CreateDecorator(IDsa wrappee)
            => new TerrainDataToContextConverter(wrappee, terrainData, context);

        protected override void PostSetup()
        {
            converter = Decorator as TerrainDataToContextConverter;
        }

        [Test]
        public void TestLengthStored()
        {
            converter.Execute();
            Assert.That(context.Length, Is.EqualTo(RESOLUTION));
        }

        [Test]
        public void TestIterationsCalculated()
        {
            converter.Execute();
            Assert.That(context.Iterations, Is.EqualTo(ITERATIONS));
        }
    }
}