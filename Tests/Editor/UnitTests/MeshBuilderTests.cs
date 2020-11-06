using NUnit.Framework;
using Moq;
using UnityEngine;
using ProceduralToolkit;

namespace UnitTests
{
    public class MeshBuilderTests
    {
        private Vector3[] expectedVertices = new Vector3[]
        {
            new Vector3(1, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 1)
        };
        private int[] expectedTriangles = new int[] { 0, 1, 2 };
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
        public void TestIfBuildedMeshIsCorrect()
        {
            resultingMesh = meshBuilder.Build();
            AssertThatResultingMeshIsCorrect();
        }

        private void AssertThatResultingMeshIsCorrect()
        {
            CollectionAssert.AreEqual(expectedVertices, resultingMesh.vertices);
            CollectionAssert.AreEqual(expectedTriangles, resultingMesh.triangles);
        }
    }
}
