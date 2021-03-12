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
            var inputVertices = new Vector3[]
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
            var expectedVertices = new Vector3[]
            {
                new Vector3(0, 7, 1),
                Vector3.zero,
                new Vector3(1, 4, 1),

                Vector3.zero,
                new Vector3(0.5f, 18, 0.5f) + Displace(),
                Vector3.zero,

                new Vector3(0, 52, 0),
                Vector3.zero,
                new Vector3(1, 9, 0)
            };
            var diamond = new Diamond(inputVertices, 0, mockDisplacer.Object);
            CollectionAssert.AreEqual(expectedVertices, diamond.Output);
        }

        [Test]
        public void TestOnNonZeroIteration()
        {
            var inputVertices = new Vector3[]
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

            var expectedVertices = new Vector3[]
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
                new Vector3(0, 1, 4) + d + Displace(),
                Vector3.zero,
                new Vector3(1, 1, 4) + d + Displace(),
                Vector3.zero,
                new Vector3(2, 1, 4) + d + Displace(),
                Vector3.zero,
                new Vector3(3, 1, 4) + d + Displace(),
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
                new Vector3(0, 1, 3) + d + Displace(),
                Vector3.zero,
                new Vector3(1, 1, 3) + d + Displace(),
                Vector3.zero,
                new Vector3(2, 1, 3) + d + Displace(),
                Vector3.zero,
                new Vector3(3, 1, 3) + d + Displace(),
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
                new Vector3(0, 1, 2) + d + Displace(),
                Vector3.zero,
                new Vector3(1, 1, 2) + d + Displace(),
                Vector3.zero,
                new Vector3(2, 1, 2) + d + Displace(),
                Vector3.zero,
                new Vector3(3, 1, 2) + d + Displace(),
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
                new Vector3(0, 1, 1) + d + Displace(),
                Vector3.zero,
                new Vector3(1, 1, 1) + d + Displace(),
                Vector3.zero,
                new Vector3(2, 1, 1) + d + Displace(),
                Vector3.zero,
                new Vector3(3, 1, 1) + d + Displace(),
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

            var diamond = new Diamond(inputVertices, 2, mockDisplacer.Object);

            CollectionAssert.AreEqual(expectedVertices, diamond.Output);
        }
    }
}