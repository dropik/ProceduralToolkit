using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DiamondTests
    {
        const int DISPLACEMENT = 4;
        private Mock<IDisplacer> mockDisplacer;

        [SetUp]
        public void Setup()
        {
            mockDisplacer = new Mock<IDisplacer>();
            mockDisplacer.Setup(m => m.GetDisplacement(It.IsAny<int>())).Returns(DISPLACEMENT);
        }

        private Vector3 Displace() => new Vector3(0, mockDisplacer.Object.GetDisplacement(2), 0);

        [Test]
        public void TestOnZeroIteration()
        {
            var vertices = new Vector3[]
            {
                new Vector3(0, 7, 1),
                Vector3.zero,
                new Vector3(1, 4, 1),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 52, 0),
                Vector3.zero,
                new Vector3(1, 9, 0)
            };
            var expectedVertices = new Vector3[9];
            vertices.CopyTo(expectedVertices, 0);
            expectedVertices[4] = new Vector3(0.5f, 18, 0.5f) + Displace();
            var diamond = new Diamond(mockDisplacer.Object);

            diamond.CalculateDiamonds(vertices, 1);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }

        [Test]
        public void TestOnNonZeroIteration()
        {
            var vertices = new Vector3[]
            {
                new Vector3(0, 1, 4),
                Vector3.zero,
                new Vector3(1, 1, 4),
                Vector3.zero,
                new Vector3(2, 1, 4),
                Vector3.zero,
                new Vector3(3, 1, 4),
                Vector3.zero,
                new Vector3(4, 1, 4),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 1, 3),
                Vector3.zero,
                new Vector3(1, 1, 3),
                Vector3.zero,
                new Vector3(2, 1, 3),
                Vector3.zero,
                new Vector3(3, 1, 3),
                Vector3.zero,
                new Vector3(4, 1, 3),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 1, 2),
                Vector3.zero,
                new Vector3(1, 1, 2),
                Vector3.zero,
                new Vector3(2, 1, 2),
                Vector3.zero,
                new Vector3(3, 1, 2),
                Vector3.zero,
                new Vector3(4, 1, 2),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 1, 1),
                Vector3.zero,
                new Vector3(1, 1, 1),
                Vector3.zero,
                new Vector3(2, 1, 1),
                Vector3.zero,
                new Vector3(3, 1, 1),
                Vector3.zero,
                new Vector3(4, 1, 1),

                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,
                Vector3.zero,

                new Vector3(0, 1, 0),
                Vector3.zero,
                new Vector3(1, 1, 0),
                Vector3.zero,
                new Vector3(2, 1, 0),
                Vector3.zero,
                new Vector3(3, 1, 0),
                Vector3.zero,
                new Vector3(4, 1, 0)
            };

            var d = new Vector3(0.5f, 0, -0.5f);

            var expectedVertices = new Vector3[81];
            vertices.CopyTo(expectedVertices, 0);
            expectedVertices[10] = new Vector3(0, 1, 4) + d + Displace();
            expectedVertices[12] = new Vector3(1, 1, 4) + d + Displace();
            expectedVertices[14] = new Vector3(2, 1, 4) + d + Displace();
            expectedVertices[16] = new Vector3(3, 1, 4) + d + Displace();

            expectedVertices[28] = new Vector3(0, 1, 3) + d + Displace();
            expectedVertices[30] = new Vector3(1, 1, 3) + d + Displace();
            expectedVertices[32] = new Vector3(2, 1, 3) + d + Displace();
            expectedVertices[34] = new Vector3(3, 1, 3) + d + Displace();

            expectedVertices[46] = new Vector3(0, 1, 2) + d + Displace();
            expectedVertices[48] = new Vector3(1, 1, 2) + d + Displace();
            expectedVertices[50] = new Vector3(2, 1, 2) + d + Displace();
            expectedVertices[52] = new Vector3(3, 1, 2) + d + Displace();

            expectedVertices[64] = new Vector3(0, 1, 1) + d + Displace();
            expectedVertices[66] = new Vector3(1, 1, 1) + d + Displace();
            expectedVertices[68] = new Vector3(2, 1, 1) + d + Displace();
            expectedVertices[70] = new Vector3(3, 1, 1) + d + Displace();

            var diamond = new Diamond(mockDisplacer.Object);
            diamond.CalculateDiamonds(vertices, 3);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }
    }
}