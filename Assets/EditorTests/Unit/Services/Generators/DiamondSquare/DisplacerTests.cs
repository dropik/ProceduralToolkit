using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class DisplacerTests
    {
        [Test]
        [TestCase(0, 0.5f, 2, 0, 2)]
        [TestCase(123, 1, 10, 2, 2.5f)]
        [TestCase(1024, 2, 100, 3, 1.5625f)]
        public void TestDisplacement(int seed, float hardness, float magnitude, int iteration, float expectedMagnitude)
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

            Assert.That(displacer.GetDisplacement(iteration), Is.EqualTo(expectedDisplacement));
        }
    }
}