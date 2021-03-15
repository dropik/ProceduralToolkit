using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DiamondDsaStepTests
    {
        const int DISPLACEMENT = 4;
        const int N = 5;

        private Vector3[] vertices;
        private Mock<IDisplacer> mockDisplacer;
        private DiamondDsaStep diamond;

        [SetUp]
        public void Setup()
        {
            vertices = new Vector3[N * N];
            vertices[0] = new Vector3(0, 7, 4);
            vertices[N - 1] = new Vector3(4, 4, 4);
            vertices[(N - 1) * N] = new Vector3(0, 52, 0);
            vertices[(N - 1) * N + N - 1] = new Vector3(4, 9, 0);

            mockDisplacer = new Mock<IDisplacer>();
            mockDisplacer.Setup(m => m.GetDisplacement(It.IsAny<int>())).Returns(DISPLACEMENT);
            diamond = new DiamondDsaStep(vertices, N, mockDisplacer.Object);
        }

        private Vector3 Displace() => new Vector3(0, mockDisplacer.Object.GetDisplacement(2), 0);

        [Test]
        public void TestOnFirstIteration()
        {
            var mid = (N - 1) / 2;
            var expectedVertices = new Vector3[N * N];
            vertices.CopyTo(expectedVertices, 0);
            expectedVertices[mid * N + mid] = new Vector3(2, 18, 2) + Displace();

            diamond.Execute(iteration: 1);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }

        [Test]
        public void TestOnSecondIteration()
        {
            vertices[0 * N + 2] = new Vector3(2, 1, 4);

            vertices[2 * N + 0] = new Vector3(0, 1, 2);
            vertices[2 * N + 2] = new Vector3(2, 1, 2);
            vertices[2 * N + 4] = new Vector3(4, 1, 2);

            vertices[4 * N + 2] = new Vector3(2, 1, 0);

            var expectedVertices = new Vector3[N * N];
            vertices.CopyTo(expectedVertices, 0);

            expectedVertices[1 * N + 1] = new Vector3(1, 2.5f, 3) + Displace();
            expectedVertices[1 * N + 3] = new Vector3(3, 1.75f, 3) + Displace();

            expectedVertices[3 * N + 1] = new Vector3(1, 13.75f, 1) + Displace();
            expectedVertices[3 * N + 3] = new Vector3(3, 3, 1) + Displace();

            diamond.Execute(iteration: 2);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }
    }
}