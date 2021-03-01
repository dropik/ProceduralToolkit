using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class DownRightAdderIT
    {
        [Test]
        public void TestDownRightVertexHeightsAdded()
        {
            var row1 = new Vector3[]
            {
                new Vector3(0, 5, 0),
                new Vector3(1, 7, 0),
                new Vector3(2, 1, 0),
                new Vector3(3, 75, 0)
            };
            var row2 = new Vector3[]
            {
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 1),
                new Vector3(2, 0, 1)
            };
            var row3 = new Vector3[]
            {
                new Vector3(0, 45, 2),
                new Vector3(1, 1, 2),
                new Vector3(2, 7, 2),
                new Vector3(3, 4, 2)
            };
            var row4 = new Vector3[]
            {
                new Vector3(0, 0, 3),
                new Vector3(1, 0, 3),
                new Vector3(2, 0, 3)
            };
            var row5 = new Vector3[]
            {
                new Vector3(0, 13, 4),
                new Vector3(1, 6, 4),
                new Vector3(2, 14, 4),
                new Vector3(3, 6, 4)
            };

            var expectedRow2 = new Vector3[]
            {
                new Vector3(0, 0.25f, 1),
                new Vector3(1, 1.75f, 1),
                new Vector3(2, 1, 1)
            };
            var expectedRow4 = new Vector3[]
            {
                new Vector3(0, 1.5f, 3),
                new Vector3(1, 3.5f, 3),
                new Vector3(2, 1.5f, 3)
            };

            var input = row1.Concat(row3).Concat(row2).Concat(row5).Concat(row4);
            var expected = row1.Concat(row3).Concat(expectedRow2).Concat(row5).Concat(expectedRow4);

            var generator = DownRightAdderFactory.Create();
            generator.InputVertices = input;
            generator.ColumnsInRow = 4;

            CollectionAssert.AreEqual(expected, generator.OutputVertices);
        }
    }
}
