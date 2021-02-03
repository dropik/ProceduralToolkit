using ProceduralToolkit.Components.GeneratorViews;
using NUnit.Framework;
using UnityEngine;
using ProceduralToolkit.Services.DI;

namespace ProceduralToolkit.EditorTests.Unit.Components.GeneratorViews
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
            CreateGameObject();
            var services = InitContainer();
            InitMeshFilter();
            InitViewWithContainer(services);
        }

        private void CreateGameObject()
        {
            obj = new GameObject();
        }

        private IServiceContainer InitContainer()
        {
            var services = ServiceContainerFactory.Create();
            services.AddSingleton<MeshFilter>(() => obj.GetComponent<MeshFilter>());
            return services;
        }

        private void InitMeshFilter()
        {
            meshFilter = obj.AddComponent<MeshFilter>();
            meshFilter.sharedMesh = new Mesh()
            {
                name = TEST_MESH_NAME_1
            };
        }

        private void InitViewWithContainer(IServiceContainer services)
        {
            view = obj.AddComponent<MeshGeneratorView>();
            services.InjectServicesTo(view);
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