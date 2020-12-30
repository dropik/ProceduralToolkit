using Moq;
using NUnit.Framework;
using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit.EditorTests.Unit
{
    public class MeshAssemblerTests
    {
        private MeshAssembler meshAssembler;
        private Mock<IMeshBuilder> mockMeshBuilder;
        private Mock<IMaterialProvider> mockMaterialProvider;
        private Mock<IMeshContainer> mockMeshContainer;
        private Mock<IMaterialContainer> mockMaterialContainer;

        private const string TEST_MESH_NAME = "Test Mesh";
        private const string TEST_MATERIAL_NAME = "Test Material";

        [SetUp]
        public void SetUp()
        {
            SetupMock();
            SetupGenerator();
        }

        private void SetupMock()
        {
            mockMeshBuilder = new Mock<IMeshBuilder>();
            mockMeshBuilder.Setup(m => m.Build())
                           .Returns(new Mesh() { name = TEST_MESH_NAME });

            mockMaterialProvider = new Mock<IMaterialProvider>();
            mockMaterialProvider.Setup(m => m.GetMaterial())
                                .Returns(new Material(Shader.Find("Standard")) { name = TEST_MATERIAL_NAME });

            mockMeshContainer = new Mock<IMeshContainer>();
            mockMaterialContainer = new Mock<IMaterialContainer>();
        }

        private void SetupGenerator()
        {
            meshAssembler = new MeshAssembler(
                mockMeshBuilder.Object,
                mockMaterialProvider.Object,
                mockMeshContainer.Object,
                mockMaterialContainer.Object
            );
        }

        [Test]
        public void TestMeshBuiltUsingBuilder()
        {
            meshAssembler.Assemble();
            mockMeshBuilder.Verify(m => m.Build(), Times.Once);
        }

        [Test]
        public void TestMeshAssignedToMeshFilter()
        {
            meshAssembler.Assemble();

            mockMeshContainer.VerifySet(
                m => m.Mesh = It.Is<Mesh>(mesh => mesh.name == TEST_MESH_NAME),
                Times.Once
            );
        }

        [Test]
        public void TestMaterialAssignedWhenWasNotSet()
        {
            mockMaterialContainer.SetupGet(m => m.Material).Returns(null as Material);

            meshAssembler.Assemble();

            mockMaterialProvider.Verify(m => m.GetMaterial(), Times.Once);
            mockMaterialContainer.VerifySet(
                m => m.Material = It.Is<Material>(material => material.name == TEST_MATERIAL_NAME),
                Times.Once
            );
        }

        [Test]
        public void TestMaterialNotModifiedWhenWasSet()
        {
            mockMaterialContainer.SetupGet(m => m.Material)
                                 .Returns(new Material(Shader.Find("Standard")));

            meshAssembler.Assemble();

            mockMaterialProvider.Verify(m => m.GetMaterial(), Times.Never);
            mockMaterialContainer.VerifySet(m => m.Material = It.IsAny<Material>(), Times.Never);
        }
    }
}
