using NUnit.Framework;
using Moq;
using UnityEngine;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services
{
    public class MeshBuilderTests
    {
        private readonly Vector3[] expectedVertices = {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1)
        };
        private readonly int[] expectedTriangles = { 0, 1, 2 };

        private Mock<IGenerator> mockGenerator;
        private MeshBuilder meshBuilder;
        private Mesh resultingMesh;

        [SetUp]
        public void SetUp()
        {
            mockGenerator = new Mock<IGenerator>();
            mockGenerator.SetupGet(m => m.Vertices)
                         .Returns(expectedVertices);
            mockGenerator.SetupGet(m => m.Triangles)
                         .Returns(expectedTriangles);
            meshBuilder = new MeshBuilder(mockGenerator.Object);
        }

        [Test]
        public void TestAddedVertices()
        {
            resultingMesh = meshBuilder.Build();
            CollectionAssert.AreEqual(expectedVertices, resultingMesh.vertices);
        }

        [Test]
        public void TestAddedTriangles()
        {
            resultingMesh = meshBuilder.Build();
            CollectionAssert.AreEqual(expectedTriangles, resultingMesh.triangles);
        }

        [Test]
        public void TestRecalculatedNormals()
        {
            resultingMesh = meshBuilder.Build();
            Assert.That(resultingMesh.normals.Length, Is.Not.Zero);
        }
    }
}
