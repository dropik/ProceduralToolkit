using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
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
            vertices[0] = new Vector3(0, 7, 4);
            vertices[N - 1] = new Vector3(4, 4, 4);
            vertices[(N - 1) * N] = new Vector3(0, 52, 0);
            vertices[(N - 1) * N + N - 1] = new Vector3(4, 9, 0);

            mockDisplacer = new Mock<IDisplacer>();
            mockDisplacer.Setup(m => m.GetDisplacement(It.IsAny<int>())).Returns(DISPLACEMENT);

            var gridSize = new Vector3(1, 0, -1);

            var context = new LandscapeContext
            {
                Vertices = vertices,
                Length = N,
                GridSize = new Vector3(1, 0, -1)
            };
            square = new SquareDsaStep(context, mockDisplacer.Object);
        }

        private Vector3 Displace() => new Vector3(0, mockDisplacer.Object.GetDisplacement(2), 0);

        [Test]
        public void TestOnFirstIteration()
        {
            var mid = (N - 1) / 2;
            vertices[mid * N + mid] = new Vector3(2, 18, 2) + Displace();

            var expectedVertices = new Vector3[N * N];
            vertices.CopyTo(expectedVertices, 0);
            expectedVertices[mid] = new Vector3(2, 13.75f, 4) + Displace();
            expectedVertices[mid * N] = new Vector3(0, 25.75f, 2) + Displace();
            expectedVertices[mid * N + N - 1] = new Vector3(4, 14.25f, 2) + Displace();
            expectedVertices[(N - 1) * N + mid] = new Vector3(2, 26.25f, 0) + Displace();

            square.Execute(iteration: 1);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }

        [Test]
        public void TestOnSecondIteration()
        {
            // 7
            vertices[0 * N + 2] = new Vector3(2, 1, 4);
            // 4

            vertices[2 * N + 0] = new Vector3(0, 1, 2);
            vertices[2 * N + 2] = new Vector3(2, 1, 2);
            vertices[2 * N + 4] = new Vector3(4, 1, 2);

            // 52
            vertices[4 * N + 2] = new Vector3(2, 1, 0);
            // 9

            vertices[1 * N + 1] = new Vector3(1, 2.5f, 3) + Displace();
            vertices[1 * N + 3] = new Vector3(3, 1.75f, 3) + Displace();

            vertices[3 * N + 1] = new Vector3(1, 13.75f, 1) + Displace();
            vertices[3 * N + 3] = new Vector3(3, 3, 1) + Displace();

            var expectedVertices = new Vector3[N * N];
            vertices.CopyTo(expectedVertices, 0);

            expectedVertices[0 * N + 1] = new Vector3(1, 8.0625f, 4) + Displace();
            expectedVertices[0 * N + 3] = new Vector3(3, 4.4375f, 4) + Displace();

            expectedVertices[1 * N + 0] = new Vector3(0, 5.0625f, 3) + Displace();
            expectedVertices[1 * N + 2] = new Vector3(2, 3.5625f, 3) + Displace();
            expectedVertices[1 * N + 4] = new Vector3(4, 4.3125f, 3) + Displace();

            expectedVertices[2 * N + 1] = new Vector3(1, 6.5625f, 2) + Displace();
            expectedVertices[2 * N + 3] = new Vector3(3, 3.6875f, 2) + Displace();

            expectedVertices[3 * N + 0] = new Vector3(0, 19.4375f, 1) + Displace();
            expectedVertices[3 * N + 2] = new Vector3(2, 6.6875f, 1) + Displace();
            expectedVertices[3 * N + 4] = new Vector3(4, 8.6875f, 1) + Displace();

            expectedVertices[4 * N + 1] = new Vector3(1, 19.3125f, 0) + Displace();
            expectedVertices[4 * N + 3] = new Vector3(3, 5.6875f, 0) + Displace();

            square.Execute(iteration: 2);

            CollectionAssert.AreEqual(expectedVertices, vertices);
        }
    }
}