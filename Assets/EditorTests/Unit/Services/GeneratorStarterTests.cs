using Moq;
using NUnit.Framework;
using ProceduralToolkit.Services.Generators.DiamondSquare;

namespace ProceduralToolkit.Services
{
    [Category("Unit")]
    public class GeneratorStarterTests
    {
        private GeneratorStarter meshAssembler;
        private Mock<IDsa> mockDsa;


        [SetUp]
        public void SetUp()
        {
            mockDsa = new Mock<IDsa>();
            meshAssembler = new GeneratorStarter(mockDsa.Object);
        }

        [Test]
        public void TestDsaExecuted()
        {
            meshAssembler.Start();
            mockDsa.Verify(m => m.Execute(), Times.Once);
        }

        [Test]
        public void TestGeneratedEventInvoked()
        {
            var invoked = false;
            meshAssembler.Generated += () => invoked = true;

            meshAssembler.Start();

            Assert.That(invoked);
        }
    }
}
