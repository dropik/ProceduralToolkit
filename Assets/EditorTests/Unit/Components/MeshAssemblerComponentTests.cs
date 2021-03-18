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
        private IServiceContainer services;
        private UpdateSettingsListener listener;

        [ExecuteInEditMode]
        internal class UpdateSettingsListener : MonoBehaviour
        {
            public bool Updated { get; set; } = false;

            private void TryUpdateSettings()
            {
                Updated = true;
            }
        }

        [SetUp]
        public void Setup()
        {
            obj = new GameObject();
            meshAssembler = obj.AddComponent<MeshAssemblerComponent>();
            listener = obj.AddComponent<UpdateSettingsListener>();

            services = ServiceContainerFactory.Create();
            mockAssembler = new Mock<IMeshAssembler>();
            services.AddSingleton(mockAssembler.Object);
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

        [Test]
        public void TestUpateSettingsMessageSent()
        {
            meshAssembler.Start();
            Assert.That(listener.Updated);
        }
    }
}