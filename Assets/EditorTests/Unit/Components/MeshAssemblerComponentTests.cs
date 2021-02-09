using System;
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

        [SetUp]
        public void Setup()
        {
            obj = new GameObject();
            meshAssembler = obj.AddComponent<MeshAssemblerComponent>();
            services = ServiceContainerFactory.Create();
            mockAssembler = new Mock<IMeshAssembler>();
            services.AddSingleton<IMeshAssembler>(mockAssembler.Object);
        }

        [TearDown]
        public void TearDown()
        {
            if (obj != null)
            {
                UnityEngine.Object.DestroyImmediate(obj);
            }
        }
            
        [Test]
        public void TestAssemblerCalledOnStart()
        {
            services.InjectServicesTo(meshAssembler);
            meshAssembler.Start();
            mockAssembler.Verify(m => m.Assemble(), Times.Once);
        }

        [Test]
        public void TestAssemblerOnStartNotInjected()
        {
            try
            {
                meshAssembler.Start();
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }
    }
}