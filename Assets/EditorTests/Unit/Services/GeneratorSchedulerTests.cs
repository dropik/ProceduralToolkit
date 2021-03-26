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
        public void TestDsaExecutedOnce()
        {
            scheduler.Update();
            scheduler.Update();
            mockDsa.Verify(m => m.Execute(), Times.Once);
        }

        [Test]
        public void TestDsaExecutedOnMarkedDirty()
        {
            scheduler.Update();
            scheduler.MarkDirty();
            scheduler.Update();
            mockDsa.Verify(m => m.Execute(), Times.Exactly(2));
        }
    }
}