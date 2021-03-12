using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DiamondTests
    {
        [Test]
        public void TestOnZeroIteration()
        {
            var inputVertices = new Vector3[]
            {
                new Vector3(0, 7, 1),
                new Vector3(1, 4, 1),
                Vector3.zero,
                new Vector3(0, 52, 0),
                new Vector3(1, 9, 0)
            };
            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 7, 1),
                new Vector3(1, 4, 1),
                new Vector3(0.5f, 18, 0.5f),
                new Vector3(0, 52, 0),
                new Vector3(1, 9, 0)
            };
            var diamond = new Diamond(inputVertices, 0);
            CollectionAssert.AreEqual(expectedVertices, diamond.Output);
        }

        [Test]
        public void TestOnNonZeroIteration()
        {
            var inputVertices = new Vector3[]
            {
                new Vector3(0, 1, 4),
                new Vector3(1, 1, 4),
                new Vector3(2, 1, 4),
                new Vector3(3, 1, 4),
                new Vector3(4, 1, 4),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 1, 3),
                new Vector3(1, 1, 3),
                new Vector3(2, 1, 3),
                new Vector3(3, 1, 3),
                new Vector3(4, 1, 3),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 1, 2),
                new Vector3(1, 1, 2),
                new Vector3(2, 1, 2),
                new Vector3(3, 1, 2),
                new Vector3(4, 1, 2),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 1, 1),
                new Vector3(1, 1, 1),
                new Vector3(2, 1, 1),
                new Vector3(3, 1, 1),
                new Vector3(4, 1, 1),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 1, 0),
                new Vector3(1, 1, 0),
                new Vector3(2, 1, 0),
                new Vector3(3, 1, 0),
                new Vector3(4, 1, 0)
            };

            var d = new Vector3(0.5f, 0, -0.5f);

            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 1, 4),
                new Vector3(1, 1, 4),
                new Vector3(2, 1, 4),
                new Vector3(3, 1, 4),
                new Vector3(4, 1, 4),

                new Vector3(0, 1, 4) + d,
                new Vector3(1, 1, 4) + d,
                new Vector3(2, 1, 4) + d,
                new Vector3(3, 1, 4) + d,

                new Vector3(0, 1, 3),
                new Vector3(1, 1, 3),
                new Vector3(2, 1, 3),
                new Vector3(3, 1, 3),
                new Vector3(4, 1, 3),

                new Vector3(0, 1, 3) + d,
                new Vector3(1, 1, 3) + d,
                new Vector3(2, 1, 3) + d,
                new Vector3(3, 1, 3) + d,

                new Vector3(0, 1, 2),
                new Vector3(1, 1, 2),
                new Vector3(2, 1, 2),
                new Vector3(3, 1, 2),
                new Vector3(4, 1, 2),

                new Vector3(0, 1, 2) + d,
                new Vector3(1, 1, 2) + d,
                new Vector3(2, 1, 2) + d,
                new Vector3(3, 1, 2) + d,

                new Vector3(0, 1, 1),
                new Vector3(1, 1, 1),
                new Vector3(2, 1, 1),
                new Vector3(3, 1, 1),
                new Vector3(4, 1, 1),

                new Vector3(0, 1, 1) + d,
                new Vector3(1, 1, 1) + d,
                new Vector3(2, 1, 1) + d,
                new Vector3(3, 1, 1) + d,

                new Vector3(0, 1, 0),
                new Vector3(1, 1, 0),
                new Vector3(2, 1, 0),
                new Vector3(3, 1, 0),
                new Vector3(4, 1, 0)
            };

            var diamond = new Diamond(inputVertices, 2);

            CollectionAssert.AreEqual(expectedVertices, diamond.Output);
        }
    }
}