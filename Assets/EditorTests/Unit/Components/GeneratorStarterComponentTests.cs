using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [Category("Unit")]
    public class GeneratorStarterComponentTests
    {
        private GameObject obj;
        private GeneratorStarterComponent meshAssembler;
        private Mock<IGeneratorStarter> mockAssembler;
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
            meshAssembler = obj.AddComponent<GeneratorStarterComponent>();
            listener = obj.AddComponent<UpdateSettingsListener>();

            services = ServiceContainerFactory.Create();
            mockAssembler = new Mock<IGeneratorStarter>();
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
            mockAssembler.Verify(m => m.Start(), Times.Once);
        }

        [Test]
        public void TestUpateSettingsMessageSent()
        {
            meshAssembler.Start();
            Assert.That(listener.Updated);
        }
    }
}