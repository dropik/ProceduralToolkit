using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class SquareTilingIT
    {
        [Test]
        public void TestTiling()
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

            var expected = new Vector3[]
            {
                new Vector3(0, 7, 2),
                new Vector3(0.5f, 3, 2),
                new Vector3(1, 3, 2),
                new Vector3(1.5f, 51, 2),
                new Vector3(2, 51, 2),

                new Vector3(0, 43, 1.5f),
                new Vector3(0.5f, 43, 1.5f),
                new Vector3(1, 12, 1.5f),
                new Vector3(1.5f, 12, 1.5f),
                new Vector3(2, 0, 1.5f),

                new Vector3(0, 14, 1),
                new Vector3(0.5f, 8, 1),
                new Vector3(1, 8, 1),
                new Vector3(1.5f, 23, 1),
                new Vector3(2, 23, 1),

                new Vector3(0, 6, 0.5f),
                new Vector3(0.5f, 6, 0.5f),
                new Vector3(1, 15, 0.5f),
                new Vector3(1.5f, 15, 0.5f),
                new Vector3(2, 0, 0.5f),

                new Vector3(0, 71, 0),
                new Vector3(0.5f, 2, 0),
                new Vector3(1, 2, 0),
                new Vector3(1.5f, 5, 0),
                new Vector3(2, 5, 0)
            };

            var tiling = SquareTilingFactory.Create();
            tiling.InputVertices = input;
            tiling.ColumnsInRow = 3;

            CollectionAssert.AreEqual(expected, tiling.OutputVertices);
        }
    }
}