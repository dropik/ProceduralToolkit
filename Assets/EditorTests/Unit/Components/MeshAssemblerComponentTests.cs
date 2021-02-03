using Moq;
using NUnit.Framework;
using ProceduralToolkit.Components;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Components
{
    [Category("Unit")]
    public class MeshAssemblerComponentTests
    {
        private GameObject obj;
        private MeshAssemblerComponent meshAssembler;
        private Mock<IMeshAssembler> mockAssembler;

        [SetUp]
        public void Setup()
        {
            obj = new GameObject();
            meshAssembler = obj.AddComponent<MeshAssemblerComponent>();
            var services = ServiceContainerFactory.Create();
            mockAssembler = new Mock<IMeshAssembler>();
            services.AddSingleton<IMeshAssembler>(mockAssembler.Object);
            services.InjectServicesTo(meshAssembler);
        }

        [TearDown]
        public void TearDown()
        {
            if (obj != null)
            {
                Object.DestroyImmediate(obj);
            }
        }
            
        [Test]
        public void TestAssemblerCalledOnStart()
        {
            meshAssembler.Start();
            mockAssembler.Verify(m => m.Assemble(), Times.Once);
        }
    }
}