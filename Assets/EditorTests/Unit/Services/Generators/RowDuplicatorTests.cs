using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class RowDuplicatorTests
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
            var duplicator = new RowDuplicator()
            {
                InputVertices = inputVertices,
                ColumnsInRow = 2
            };
            CollectionAssert.AreEqual(expectedVertices, duplicator.OutputVertices);
        }

        [Test]
        public void TestOnInputVerticesNotSet()
        {
            var duplicator = new RowDuplicator()
            {
                ColumnsInRow = 2
            };
            var expectedVertices = new Vector3[0];
            CollectionAssert.AreEqual(expectedVertices, duplicator.OutputVertices);
        }

        [Test]
        public void TestOnColumnsInRowNotSet()
        {
            var inputVertices = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(2, 0, 0)
            };
            var duplicator = new RowDuplicator()
            {
                InputVertices = inputVertices
            };
            CollectionAssert.AreEqual(inputVertices, duplicator.OutputVertices);
        }

        [Test]
        public void TestOnNegativeColumnsInRow()
        {
            var inputVertices = new Vector3[]
           {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(2, 0, 0)
           };
            var duplicator = new RowDuplicator()
            {
                InputVertices = inputVertices,
                ColumnsInRow = -2
            };
            CollectionAssert.AreEqual(inputVertices, duplicator.OutputVertices);
        }
    }
}
