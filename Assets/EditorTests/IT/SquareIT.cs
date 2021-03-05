using NUnit.Framework;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class SquareIT
    {
        [Test]
        public void TestSquaresPart()
        {
            var input = new Vector3[]
            {
                new Vector3(0, 7, 2),
                new Vector3(1, 3, 2),
                new Vector3(2, 51, 2),

                new Vector3(0.5f, 43, 1.5f),
                new Vector3(1.5f, 12, 1.5f),

                new Vector3(0, 14, 1),
                new Vector3(1, 8, 1),
                new Vector3(2, 23, 1),

                new Vector3(0.5f, 6, 0.5f),
                new Vector3(1.5f, 15, 0.5f),

                new Vector3(0, 71, 0),
                new Vector3(1, 2, 0),
                new Vector3(2, 5, 0)
            };

            Random.InitState(0);
            Vector3 elevate() => new Vector3(0, Random.Range(-1f, 1f), 0);

            var expected = new Vector3[]
            {
                new Vector3(0, 7, 2),
                new Vector3(0.5f, 53/3f, 2) + elevate(),
                new Vector3(1, 3, 2),
                new Vector3(1.5f, 66/3f, 2) + elevate(),
                new Vector3(2, 51, 2),

                new Vector3(0, 64/3f, 1.5f) + elevate(),
                new Vector3(0.5f, 43, 1.5f),
                new Vector3(1, 66/4f, 1.5f) + elevate(),
                new Vector3(1.5f, 12, 1.5f),
                new Vector3(2, 86/3f, 1.5f) + elevate(),

                new Vector3(0, 14, 1),
                new Vector3(0.5f, 71/4f, 1) + elevate(),
                new Vector3(1, 8, 1),
                new Vector3(1.5f, 58/4f, 1) + elevate(),
                new Vector3(2, 23, 1),

                new Vector3(0, 91/3f, 0.5f) + elevate(),
                new Vector3(0.5f, 6, 0.5f),
                new Vector3(1, 31/4f, 0.5f) + elevate(),
                new Vector3(1.5f, 15, 0.5f),
                new Vector3(2, 43/3f, 0.5f) + elevate(),

                new Vector3(0, 71, 0),
                new Vector3(0.5f, 79/3f, 0) + elevate(),
                new Vector3(1, 2, 0),
                new Vector3(1.5f, 22/3f, 0) + elevate(),
                new Vector3(2, 5, 0)
            };

            var settings = new DSASettings { Seed = 0, Hardness = 0, Magnitude = 1 };
            var square = new Services.Generators.Square { Settings = settings, InputVertices = input, Iteration = 1 };

            CollectionAssert.AreEqual(expected, square.OutputVertices);
        }
    }
}