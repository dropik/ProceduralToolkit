using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class DiamondSquareIterationIT
    {
        [Test]
        public void TestDiamondSquareIteration()
        {
            var input = new Vector3[]
            {
                new Vector3(0, 7, 2),
                new Vector3(1, 3, 2),
                new Vector3(2, 51, 2),

                new Vector3(0, 14, 1),
                new Vector3(1, 8, 1),
                new Vector3(2, 23, 1),

                new Vector3(0, 71, 0),
                new Vector3(1, 2, 0),
                new Vector3(2, 5, 0)
            };

            var expected = new Vector3[]
            {
                new Vector3(0, 7, 2),
                new Vector3(0.5f, 18/3f, 2),
                new Vector3(1, 3, 2),
                new Vector3(1.5f, 75.25f/3f, 2),
                new Vector3(2, 51, 2),

                new Vector3(0, 29/3f, 1.5f),
                new Vector3(0.5f, 8, 1.5f),
                new Vector3(1, 40.25f/4f, 1.5f),
                new Vector3(1.5f, 21.25f, 1.5f),
                new Vector3(2, 95.25f/3f, 1.5f),

                new Vector3(0, 14, 1),
                new Vector3(0.5f, 53.75f/4f, 1),
                new Vector3(1, 8, 1),
                new Vector3(1.5f, 61.75f/4f, 1),
                new Vector3(2, 23, 1),

                new Vector3(0, 108.75f/3f, 0.5f),
                new Vector3(0.5f, 23.75f, 0.5f),
                new Vector3(1, 43.25f/4f, 0.5f),
                new Vector3(1.5f, 9.5f, 0.5f),
                new Vector3(2, 37.5f/3f, 0.5f),

                new Vector3(0, 71, 0),
                new Vector3(0.5f, 96.75f/3f, 0),
                new Vector3(1, 2, 0),
                new Vector3(1.5f, 16.5f/3f, 0),
                new Vector3(2, 5, 0)
            };

            var settings = new DSASettings { Seed = 0, Hardness = 0, Magnitude = 0 };
            var ds = new DiamondSquareIteration { Settings = settings, InputVertices = input, Iteration = 1 };

            CollectionAssert.AreEqual(expected, ds.OutputVertices);
        }
    }
}