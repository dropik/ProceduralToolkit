using NUnit.Framework;
using ProceduralToolkit.Components;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Components
{
    [Category("Unit")]
    public class MeshGeneratorViewTests
    {
        private GameObject obj;
        private MeshFilter meshFilter;
        private MeshGeneratorView view;
        
        private Mesh Mesh2 => new Mesh() { name = TEST_MESH_NAME_2 };

        private const string TEST_MESH_NAME_1 = "Test Mesh 1";
        private const string TEST_MESH_NAME_2 = "Test Mesh 2";

        [SetUp]
        public void SetUp()
        {
            InitGameObject();
            InjectServices();
        }

        private void InitGameObject()
        {
            CreateGameObject();
            view = obj.AddComponent<MeshGeneratorView>();
            InitMeshFilter();
        }

        private void InjectServices()
        {
            var services = InitContainer();
            services.InjectServicesTo(view);
        }

        private void CreateGameObject()
        {
            obj = new GameObject();
        }

        private void InitMeshFilter()
        {
            meshFilter = obj.GetComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh()
            {
                name = TEST_MESH_NAME_1
            };
        }

        private IServiceContainer InitContainer()
        {
            var services = ServiceContainerFactory.Create();
            services.AddSingleton(() => obj.GetComponent<MeshFilter>());
            return services;
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(obj);
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
            view.NewMesh = Mesh2;
            view.Update();
            Assert.That(meshFilter.sharedMesh.name, Is.EqualTo(TEST_MESH_NAME_2));
        }

        [Test]
        public void TestNewMeshNulledAfterUpdate()
        {
            view.NewMesh = Mesh2;
            view.Update();
            Assert.That(view.NewMesh, Is.Null);
        }
    }
}