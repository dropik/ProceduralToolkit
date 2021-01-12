using NUnit.Framework;
using UnityEngine;
using ProceduralToolkit.Services;

namespace ProceduralToolkit.EditorTests.Unit.Services
{
    [Category("Unit")]
    public class GeneratorViewTests
    {
        private MeshFilter meshFilter;
        private GeneratorView view;

        private const string TEST_MESH_NAME_1 = "Test Mesh 1";
        private const string TEST_MESH_NAME_2 = "Test Mesh 2";

        [SetUp]
        public void SetUp()
        {
            meshFilter = new GameObject().AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh()
            {
                name = TEST_MESH_NAME_1
            };
            view = new GeneratorView(meshFilter);
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(meshFilter.gameObject);
        }

        [Test]
        public void TestMeshFilterNotUpdatedOnNullNewMesh()
        {
            view.Update();
            Assert.That(meshFilter.sharedMesh.name, Is.EqualTo(TEST_MESH_NAME_1));
        }

        [Test]
        public void TestMeshFilterUpdatedOnNewMesh()
        {
            SetNewMesh();
            view.Update();
            Assert.That(meshFilter.sharedMesh.name, Is.EqualTo(TEST_MESH_NAME_2));
        }

        private void SetNewMesh()
        {
            view.NewMesh = new Mesh()
            {
                name = TEST_MESH_NAME_2
            };
        }

        [Test]
        public void TestNewMeshNulledAfterUpdate()
        {
            SetNewMesh();
            view.Update();
            Assert.That(view.NewMesh, Is.Null);
        }
    }
}
