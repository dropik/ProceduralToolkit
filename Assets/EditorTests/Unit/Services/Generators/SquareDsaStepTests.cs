using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class SquareDsaStepTests
    {
        const int DISPLACEMENT = 4;
        const int N = 5;

        private Vector3[] vertices;
        private Mock<IDisplacer> mockDisplacer;
        private SquareDsaStep square;

        [SetUp]
        public void Setup()
        {
            vertices = new Vector3[N * N];
            mockDisplacer = new Mock<IDisplacer>();
            mockDisplacer.Setup(m => m.GetDisplacement(It.IsAny<int>())).Returns(DISPLACEMENT);
            square = new SquareDsaStep(vertices, mockDisplacer.Object);
        }

        private Vector3 Displace() => new Vector3(0, mockDisplacer.Object.GetDisplacement(2), 0);

        [Test]
        public void TestOnFirstIteration()
        {
            var mid = (N - 1) / 2;
            vertices[0] = new Vector3(0, 7, 1);
            vertices[N - 1] = new Vector3(1, 4, 1);
            vertices[(N - 1) * N] = new Vector3(0, 52, 0);
            vertices[(N - 1) * N + N - 1] = new Vector3(1, 9, 0);
            vertices[mid * N + mid] = new Vector3(0.5f, 18, 0.5f) + Displace();

            var expectedVertices = new Vector3[N * N];
            vertices.CopyTo(expectedVertices, 0);
            expectedVertices[mid] = new Vector3(0.5f, 13.75f, 1) + Displace();
            expectedVertices[mid * N] = new Vector3(0, 25.75f, 0.5f) + Displace();
            expectedVertices[mid * N + N - 1] = new Vector3(1, 14.25f, 0.5f) + Displace();
            expectedVertices[(N - 1) * N + mid] = new Vector3(0.5f, 26.25f, 0) + Displace();

            square.Execute(iteration: 1);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }

        [Test]
        public void TestOnSecondIteration()
        {
            vertices[0 * N + 0] = new Vector3(0, 1, 4);
            vertices[0 * N + 2] = new Vector3(2, 1, 4);
            vertices[0 * N + 4] = new Vector3(4, 1, 4);

            vertices[2 * N + 0] = new Vector3(0, 1, 2);
            vertices[2 * N + 2] = new Vector3(2, 1, 2);
            vertices[2 * N + 4] = new Vector3(4, 1, 2);

            vertices[4 * N + 0] = new Vector3(0, 1, 0);
            vertices[4 * N + 2] = new Vector3(2, 1, 0);
            vertices[4 * N + 4] = new Vector3(4, 1, 0);

            vertices[1 * N + 1] = new Vector3(1, 1, 3) + Displace();
            vertices[1 * N + 3] = new Vector3(3, 1, 3) + Displace();

            vertices[3 * N + 1] = new Vector3(1, 1, 1) + Displace();
            vertices[3 * N + 3] = new Vector3(3, 1, 1) + Displace();

            var expectedVertices = new Vector3[N * N];
            vertices.CopyTo(expectedVertices, 0);

            expectedVertices[0 * N + 1] = new Vector3(1, 3, 4) + Displace();
            expectedVertices[0 * N + 3] = new Vector3(3, 3, 4) + Displace();

            expectedVertices[1 * N + 0] = new Vector3(0, 3, 3) + Displace();
            expectedVertices[1 * N + 2] = new Vector3(2, 3, 3) + Displace();
            expectedVertices[1 * N + 4] = new Vector3(4, 3, 3) + Displace();

            expectedVertices[2 * N + 1] = new Vector3(1, 3, 2) + Displace();
            expectedVertices[2 * N + 3] = new Vector3(3, 3, 2) + Displace();

            expectedVertices[3 * N + 0] = new Vector3(0, 3, 1) + Displace();
            expectedVertices[3 * N + 2] = new Vector3(2, 3, 1) + Displace();
            expectedVertices[3 * N + 4] = new Vector3(4, 3, 1) + Displace();

            expectedVertices[4 * N + 1] = new Vector3(1, 3, 0) + Displace();
            expectedVertices[4 * N + 3] = new Vector3(3, 3, 0) + Displace();

            square.Execute(iteration: 2);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }
    }
}