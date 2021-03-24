using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class DisplacerTests
    {
        [Test]
        [TestCase(0, 0.5f, 0.1f, 0, 0.05f)]
        [TestCase(123, 1, 0.5f, 2, 0.0625f)]
        [TestCase(1024, 2, 1, 3, 0.0078125f)]
        public void TestNormalizedDisplacement(int seed, float hardness, float magnitude, int iteration, float expectedMagnitude)
        {
            var settings = new DsaSettings
            {
                Seed = seed,
                Hardness = hardness,
                Magnitude = magnitude
            };

            Random.InitState(seed);
            var expectedDisplacement = Random.Range(-expectedMagnitude, expectedMagnitude);
            Random.InitState(seed);
            var displacer = new Displacer(settings);

            Assert.That(displacer.GetNormalizedDisplacement(iteration), Is.EqualTo(expectedDisplacement));
        }
    }
}