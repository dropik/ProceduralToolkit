using System;
using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators.DiamondSquare;

namespace ProceduralToolkit.Services
{
    [Category("Unit")]
    public class GeneratorSchedulerTests
    {
        private GeneratorScheduler scheduler;
        private Mock<IDsa> mockDsa;

        [SetUp]
        public void Setup()
        {
            mockDsa = new Mock<IDsa>();
            scheduler = new GeneratorScheduler(mockDsa.Object);
        }

        [Test]
        public void TestDsaNotExecutedOnUpdateIfNotDirty()
        {
            scheduler.Update();
            mockDsa.Verify(m => m.Execute(), Times.Never);
        }

        [Test]
        public void TestDsaExecutedOnceOnOneDirtyMark()
        {
            scheduler.MarkDirty();
            scheduler.Update();
            scheduler.Update();
            mockDsa.Verify(m => m.Execute(), Times.Once);
        }

        [Test]
        public void TestGeneratedEventNotInvokedIfNotDirty()
        {
            var mockGeneratedAction = new Mock<Action>();
            scheduler.Generated += mockGeneratedAction.Object;

            scheduler.Update();

            mockGeneratedAction.Verify(m => m.Invoke(), Times.Never);
        }

        [Test]
        public void TestGeneratedEventInvokedOnceOnOneDirtyMark()
        {
            var mockGeneratedAction = new Mock<Action>();
            scheduler.Generated += mockGeneratedAction.Object;

            scheduler.MarkDirty();
            scheduler.Update();
            scheduler.Update();

            mockGeneratedAction.Verify(m => m.Invoke(), Times.Once);
        }

        [Test]
        public void TestOnGeneratedCallbacksNotSet()
        {
            scheduler.MarkDirty();
            scheduler.Update();
        }
    }
}