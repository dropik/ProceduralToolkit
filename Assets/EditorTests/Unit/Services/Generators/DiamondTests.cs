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
        const int N = 5;

        private Vector3[] vertices;
        private Mock<IDisplacer> mockDisplacer;
        private Diamond diamond;

        [SetUp]
        public void Setup()
        {
            vertices = new Vector3[N * N];
            mockDisplacer = new Mock<IDisplacer>();
            mockDisplacer.Setup(m => m.GetDisplacement(It.IsAny<int>())).Returns(DISPLACEMENT);
            diamond = new Diamond(vertices, mockDisplacer.Object);
        }

        private Vector3 Displace() => new Vector3(0, mockDisplacer.Object.GetDisplacement(2), 0);

        [Test]
        public void TestOnFirstIteration()
        {
            vertices[0] = new Vector3(0, 7, 1);
            vertices[N - 1] = new Vector3(1, 4, 1);
            vertices[(N - 1) * N] = new Vector3(0, 52, 0);
            vertices[(N - 1) * N + N - 1] = new Vector3(1, 9, 0);

            var expectedVertices = new Vector3[N * N];
            vertices.CopyTo(expectedVertices, 0);
            expectedVertices[2 * N + 2] = new Vector3(0.5f, 18, 0.5f) + Displace();

            diamond.Execute(iteration: 1);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }

        [Test]
        public void TestOnSecondIteration()
        {
            vertices[0 * N + 0] = new Vector3(0, 1, 4);
            vertices[0 * N + 2] = new Vector3(1, 1, 4);
            vertices[0 * N + 4] = new Vector3(2, 1, 4);

            vertices[2 * N + 0] = new Vector3(0, 1, 3);
            vertices[2 * N + 2] = new Vector3(1, 1, 3);
            vertices[2 * N + 4] = new Vector3(2, 1, 3);

            vertices[4 * N + 0] = new Vector3(0, 1, 2);
            vertices[4 * N + 2] = new Vector3(1, 1, 2);
            vertices[4 * N + 4] = new Vector3(2, 1, 2);

            var d = new Vector3(0.5f, 0, -0.5f);

            var expectedVertices = new Vector3[N * N];
            vertices.CopyTo(expectedVertices, 0);

            expectedVertices[1 * N + 1] = new Vector3(0, 1, 4) + d + Displace();
            expectedVertices[1 * N + 3] = new Vector3(1, 1, 4) + d + Displace();

            expectedVertices[3 * N + 1] = new Vector3(0, 1, 3) + d + Displace();
            expectedVertices[3 * N + 3] = new Vector3(1, 1, 3) + d + Displace();

            diamond.Execute(iteration: 2);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }
    }
}