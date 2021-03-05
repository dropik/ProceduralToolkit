using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class NormalizerIT
    {
        [Test]
        public void TestSquaresNormalized()
        {
            var input = new Vector3[]
            {
                new Vector3(0, 7, 2),
                new Vector3(0.5f, 53, 2),
                new Vector3(1, 3, 2),
                new Vector3(1.5f, 66, 2),
                new Vector3(2, 51, 2),

                new Vector3(0, 64, 1.5f),
                new Vector3(0.5f, 43, 1.5f),
                new Vector3(1, 66, 1.5f),
                new Vector3(1.5f, 12, 1.5f),
                new Vector3(2, 86, 1.5f),

                new Vector3(0, 14, 1),
                new Vector3(0.5f, 71, 1),
                new Vector3(1, 8, 1),
                new Vector3(1.5f, 58, 1),
                new Vector3(2, 23, 1),

                new Vector3(0, 91, 0.5f),
                new Vector3(0.5f, 6, 0.5f),
                new Vector3(1, 31, 0.5f),
                new Vector3(1.5f, 15, 0.5f),
                new Vector3(2, 43, 0.5f),

                new Vector3(0, 71, 0),
                new Vector3(0.5f, 79, 0),
                new Vector3(1, 2, 0),
                new Vector3(1.5f, 22, 0),
                new Vector3(2, 5, 0)
            };

            var expected = new Vector3[]
            {
                new Vector3(0, 7, 2),
                new Vector3(0.5f, 53/3f, 2),
                new Vector3(1, 3, 2),
                new Vector3(1.5f, 66/3f, 2),
                new Vector3(2, 51, 2),

                new Vector3(0, 64/3f, 1.5f),
                new Vector3(0.5f, 43, 1.5f),
                new Vector3(1, 66/4f, 1.5f),
                new Vector3(1.5f, 12, 1.5f),
                new Vector3(2, 86/3f, 1.5f),

                new Vector3(0, 14, 1),
                new Vector3(0.5f, 71/4f, 1),
                new Vector3(1, 8, 1),
                new Vector3(1.5f, 58/4f, 1),
                new Vector3(2, 23, 1),

                new Vector3(0, 91/3f, 0.5f),
                new Vector3(0.5f, 6, 0.5f),
                new Vector3(1, 31/4f, 0.5f),
                new Vector3(1.5f, 15, 0.5f),
                new Vector3(2, 43/3f, 0.5f),

                new Vector3(0, 71, 0),
                new Vector3(0.5f, 79/3f, 0),
                new Vector3(1, 2, 0),
                new Vector3(1.5f, 22/3f, 0),
                new Vector3(2, 5, 0)
            };

            var normalizer = NormalizerFactory.Create();
            normalizer.InputVertices = input;
            normalizer.ColumnsInRow = 5;

            CollectionAssert.AreEqual(expected, normalizer.OutputVertices);
        }
    }
}