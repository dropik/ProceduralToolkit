using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class DiamondTilingIT
    {
        [Test]
        public void TestTiling()
        {
            var firstRow = new Vector3[]
            {
                new Vector3(0, 5, 3),
                new Vector3(1, 2, 3),
                new Vector3(2, 87, 3),
                new Vector3(3, 2, 3)
            };
            var secondRow = new Vector3[]
            {
                new Vector3(0, 2, 2),
                new Vector3(1, 3, 2),
                new Vector3(2, 6, 2),
                new Vector3(3, 9, 2)
            };
            var inputVertices = firstRow.Concat(secondRow).Concat(secondRow);

            var d = new Vector3(0.5f, 0, -0.5f);
            var expectedMiddle = new Vector3[]
            {
                new Vector3(0, 0, 3) + d,
                new Vector3(1, 0, 3) + d,
                new Vector3(2, 0, 3) + d
            };
            var expectedVertices = firstRow.Concat(expectedMiddle).Concat(secondRow);

            var tiling = DiamondTilingFactory.Create();
            tiling.InputVertices = inputVertices;
            tiling.ColumnsInRow = 4;
            CollectionAssert.AreEqual(expectedVertices, tiling.OutputVertices);
        }
    }
}