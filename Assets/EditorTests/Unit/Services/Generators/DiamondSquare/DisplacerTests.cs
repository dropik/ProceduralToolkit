using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    [Category("Unit")]
    public class DisplacerTests
    {
        [Test]
        [TestCase(0, 0.5f, 0, 0.5f)]
        [TestCase(123, 1, 2, 0.125f)]
        [TestCase(1024, 2, 3, 0.0078125f)]
        public void TestNormalizedDisplacement(int seed, float hardness, int iteration, float expectedMagnitude)
        {
            var settings = new DsaSettings
            {
                Seed = seed,
                Hardness = hardness
            };

            Random.InitState(seed);
            var expectedDisplacement = Random.Range(-expectedMagnitude, expectedMagnitude);
            Random.InitState(seed);
            var displacer = new Displacer(settings);

            Assert.That(displacer.GetNormalizedDisplacement(iteration), Is.EqualTo(expectedDisplacement));
        }
    }
}