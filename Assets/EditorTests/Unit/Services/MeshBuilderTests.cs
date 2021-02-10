using NUnit.Framework;
using UnityEngine;
using ProceduralToolkit.Services;
using ProceduralToolkit.Models;

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
        private readonly Triangle[] inputTriangles = new Triangle[]
        {
            new Triangle(0, 1, 2)
        };
        private readonly int[] expectedTriangles = { 0, 1, 2 };

        private MeshBuilder meshBuilder;

        [SetUp]
        public void SetUp()
        {
            meshBuilder = new MeshBuilder(() => expectedVertices, () => inputTriangles);
        }

        [Test]
        public void TestAddedVertices()
        {
            var resultingMesh = meshBuilder.Build();
            CollectionAssert.AreEqual(expectedVertices, resultingMesh.vertices);
        }

        [Test]
        public void TestAddedTriangles()
        {
            var resultingMesh = meshBuilder.Build();
            CollectionAssert.AreEqual(expectedTriangles, resultingMesh.triangles);
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
            var trianglesInSquare = new Triangle[]
            {
                new Triangle(0, 1, 2),
                new Triangle(0, 2, 3)
            };
            var expectedTriangles = new int[]
            {
                0, 1, 2,
                0, 2, 3
            };
            meshBuilder = new MeshBuilder(() => verticesInSquare, () => trianglesInSquare);
            var resultingMesh = meshBuilder.Build();
            CollectionAssert.AreEqual(verticesInSquare, resultingMesh.vertices);
            CollectionAssert.AreEqual(expectedTriangles, resultingMesh.triangles);
        }
    }
}
