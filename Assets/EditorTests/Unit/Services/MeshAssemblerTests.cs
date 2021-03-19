using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators.DiamondSquare;
using UnityEngine;

namespace ProceduralToolkit.Services
{
    [Category("Unit")]
    public class MeshAssemblerTests
    {
        private MeshAssembler meshAssembler;
        private Mock<IMeshBuilder> mockMeshBuilder;
        private Mock<IDsa> mockDsa;

        private const string TEST_MESH_NAME = "Test Mesh";

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

            mockDsa = new Mock<IDsa>();
        }

        private void SetupGenerator()
        {
            meshAssembler = new MeshAssembler(mockMeshBuilder.Object, mockDsa.Object);
        }

        [Test]
        public void TestDsaExecuted()
        {
            meshAssembler.Assemble();
            mockDsa.Verify(m => m.Execute(), Times.Once);
        }

        [Test]
        public void TestMeshBuiltUsingBuilder()
        {
            meshAssembler.Assemble();
            mockMeshBuilder.Verify(m => m.Build(), Times.Once);
        }

        [Test]
        public void TestGeneratedInvoked()
        {
            var invoked = false;
            meshAssembler.Generated += (mesh) => invoked = true;

            meshAssembler.Assemble();

            Assert.That(invoked);
        }
    }
}
