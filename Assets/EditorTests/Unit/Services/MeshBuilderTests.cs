using NUnit.Framework;
using UnityEngine;
using ProceduralToolkit.Services;

namespace ProceduralToolkit.EditorTests.Unit.Services
{
    [Category("Unit")]
    public class MeshBuilderTests
    {
        private readonly Vector3[] expectedVertices = {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1)
        };
        private readonly int[] expectedIndices = { 0, 1, 2 };

        private MeshBuilder meshBuilder;

        [SetUp]
        public void SetUp()
        {
            meshBuilder = new MeshBuilder(() => (expectedVertices, expectedIndices));
        }

        [Test]
        public void TestAddedVertices()
        {
            var resultingMesh = meshBuilder.Build();
            CollectionAssert.AreEqual(expectedVertices, resultingMesh.vertices);
        }

        [Test]
        public void TestAddedIndices()
        {
            var resultingMesh = meshBuilder.Build();
            CollectionAssert.AreEqual(expectedIndices, resultingMesh.GetIndices(0));
        }

        [Test]
        public void TestTopologyIsTriangles()
        {
            var resultingMesh = meshBuilder.Build();
            Assert.That(resultingMesh.GetTopology(0), Is.EqualTo(MeshTopology.Triangles));
        }

        [Test]
        public void TestRecalculatedNormals()
        {
            var resultingMesh = meshBuilder.Build();
            Assert.That(resultingMesh.normals.Length, Is.Not.Zero);
        }

        [Test]
        public void TestOnDifferentSizes()
        {
            var verticesInSquare = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(0, 1, 0),
                new Vector3(1, 1, 0),
                new Vector3(1, 0, 0)
            };
            var indicesInSquare = new int[]
            {
                0, 1, 2,
                0, 2, 3
            };
            meshBuilder = new MeshBuilder(() => (verticesInSquare, indicesInSquare));

            var resultingMesh = meshBuilder.Build();

            CollectionAssert.AreEqual(verticesInSquare, resultingMesh.vertices);
            CollectionAssert.AreEqual(indicesInSquare, resultingMesh.GetIndices(0));
        }
    }
}
