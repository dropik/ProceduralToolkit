using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class InvertorIT
    {
        [Test]
        public void TestRowsInverted()
        {
            var row1 = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(2, 0, 0),
                new Vector3(3, 0, 0)
            };
            var row2 = new Vector3[]
            {
                new Vector3(0, 0, 1),
                new Vector3(1, 0, 1),
                new Vector3(2, 0, 1)
            };
            var row3 = new Vector3[]
            {
                new Vector3(0, 0, 2),
                new Vector3(1, 0, 2),
                new Vector3(2, 0, 2),
                new Vector3(3, 0, 2)
            };
            var row4 = new Vector3[]
            {
                new Vector3(0, 0, 3),
                new Vector3(1, 0, 3),
                new Vector3(2, 0, 3),
            };
            var row5 = new Vector3[]
            {
                new Vector3(0, 0, 4),
                new Vector3(1, 0, 4),
                new Vector3(2, 0, 4),
                new Vector3(3, 0, 4)
            };
            var input = row1.Concat(row2).Concat(row3).Concat(row4).Concat(row5).Concat(new Vector3[3]);
            var expectedOuptut = row1.Concat(row3).Concat(row2).Concat(row5).Concat(row4);

            var invertor = InvertorFactory.Create();
            invertor.InputVertices = input;
            invertor.ColumnsInRow = 4;

            CollectionAssert.AreEqual(expectedOuptut, invertor.OutputVertices);
        }
    }
}
