using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class RowDuplicatorIT
    {
        [Test]
        public void TestRowsAreDuplicated()
        {
            var row1 = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0)
            };
            var row2 = new Vector3[]
            {
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 1)
            };
            var row3 = new Vector3[]
            {
                new Vector3(0, 0, 2),
                new Vector3(1, 0, 2)
            };
            var inputVertices = row1.Concat(row2).Concat(row3);
            var expectedVertices = row1.Concat(row2).Concat(row2).Concat(row3).Concat(row3);
            var duplicator = RowDuplicatorFactory.Create();
            duplicator.InputVertices = inputVertices;
            duplicator.ColumnsInRow = 2;
            CollectionAssert.AreEqual(expectedVertices, duplicator.OutputVertices);
        }
    }
}
