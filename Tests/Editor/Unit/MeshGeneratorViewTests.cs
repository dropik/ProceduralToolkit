using Moq;
using NUnit.Framework;
using ProceduralToolkit.Api;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit
{
    public class MeshGeneratorViewTests
    {
        private Mock<IMeshContainer> mockMeshContainer;
        private MeshGeneratorView view;

        private const string TEST_MESH_NAME = "Test Mesh";

        [SetUp]
        public void SetUp()
        {
            mockMeshContainer = new Mock<IMeshContainer>();
            view = new MeshGeneratorView(mockMeshContainer.Object);
        }

        [Test]
        public void TestMeshAssignedToContainer()
        {
            var testMesh = new Mesh()
            {
                name = TEST_MESH_NAME
            };

            view.OnGenerate(testMesh);

            mockMeshContainer.VerifySet(
                m => m.Mesh = It.Is<Mesh>(mesh => mesh.name == TEST_MESH_NAME),
                Times.Once
            );
        }
    }
}
