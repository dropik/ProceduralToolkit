using Moq;
using NUnit.Framework;

namespace ProceduralToolkit.Services
{
    [Category("Unit")]
    public class GeneratorSchedulerTests
    {
        private GeneratorScheduler meshAssembler;
        private Mock<IGeneratorStarter> mockAssembler;

        [SetUp]
        public void Setup()
        {
            mockAssembler = new Mock<IGeneratorStarter>();
            meshAssembler = new GeneratorScheduler(mockAssembler.Object);
        }

        [Test]
        public void TestAssemblerNotCalledOnUpdateIfNotDirty()
        {
            meshAssembler.Update();
            mockAssembler.Verify(m => m.Start(), Times.Never);
        }

        [Test]
        public void TestAssemblerCalledOnceOnOneDirtyMark()
        {
            meshAssembler.MarkDirty();
            meshAssembler.Update();
            meshAssembler.Update();
            mockAssembler.Verify(m => m.Start(), Times.Once);
        }
    }
}