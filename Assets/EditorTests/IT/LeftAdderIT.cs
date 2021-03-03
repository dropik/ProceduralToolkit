using NUnit.Framework;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.IT
{
    [Category("IT")]
    public class LeftAdderIT
    {
        [Test]
        public void TestAddedHeightFromLeft()
        {
            var input = new Vector3[]
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

            var expected = new Vector3[]
            {
                new Vector3(0, 7, 2),
                new Vector3(0.5f, 10, 2),
                new Vector3(1, 3, 2),
                new Vector3(1.5f, 54, 2),
                new Vector3(2, 51, 2),

                new Vector3(0, 43, 1.5f),
                new Vector3(0.5f, 43, 1.5f),
                new Vector3(1, 55, 1.5f),
                new Vector3(1.5f, 12, 1.5f),
                new Vector3(2, 12, 1.5f),

                new Vector3(0, 14, 1),
                new Vector3(0.5f, 22, 1),
                new Vector3(1, 8, 1),
                new Vector3(1.5f, 31, 1),
                new Vector3(2, 23, 1),

                new Vector3(0, 6, 0.5f),
                new Vector3(0.5f, 6, 0.5f),
                new Vector3(1, 21, 0.5f),
                new Vector3(1.5f, 15, 0.5f),
                new Vector3(2, 15, 0.5f),

                new Vector3(0, 71, 0),
                new Vector3(0.5f, 73, 0),
                new Vector3(1, 2, 0),
                new Vector3(1.5f, 7, 0),
                new Vector3(2, 5, 0)
            };

            var adder = LeftAdderFactory.Create();
            adder.InputVertices = input;
            adder.ColumnsInRow = 5;

            CollectionAssert.AreEqual(expected, adder.OutputVertices);
        }
    }
}