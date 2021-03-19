using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.Services
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

        private LandscapeContext context;
        private Mock<IIndicesGenerator> mockIndicesGenerator;

        private MeshBuilder meshBuilder;

        [SetUp]
        public void SetUp()
        {
            context = new LandscapeContext()
            {
                Vertices = expectedVertices
            };

            mockIndicesGenerator = new Mock<IIndicesGenerator>();
            mockIndicesGenerator.Setup(m => m.Execute()).Callback(() => context.Indices = expectedIndices);

            meshBuilder = new MeshBuilder(context, mockIndicesGenerator.Object);
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
    }
}
