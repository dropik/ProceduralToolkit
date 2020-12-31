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
        }

        private void SetupGenerator()
        {
            meshAssembler = new MeshAssembler(mockMeshBuilder.Object);
        }

        [Test]
        public void TestMeshBuiltUsingBuilder()
        {
            meshAssembler.Assemble();
            mockMeshBuilder.Verify(m => m.Build(), Times.Once);
        }

        [Test]
        public void TestGeneratedInvokeOkWhenHasNoCallbacks()
        {
            try
            {
                meshAssembler.Assemble();
                Assert.Pass();
            }
            catch (System.NullReferenceException)
            {
                Assert.Fail();
            }
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
