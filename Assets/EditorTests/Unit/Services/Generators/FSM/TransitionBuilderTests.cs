using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class TransitionBuilderTests
    {
        private Mock<IStateBuilder> mockBuilder;

        [SetUp]
        public void Setup()
        {
            mockBuilder = new Mock<IStateBuilder>();
            mockBuilder.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
        }

        [Test]
        public void TestNextStateIsSet()
        {
            const string name = "state";
            var transition = new Transition();
            var builder = new TransitionBuilder(null as IStateBuilder, transition);

            builder.SetNext(name);

            Assert.That(transition.NextState, Is.EqualTo(name));
        }

        [Test]
        public void TestGivenBuilderReturned()
        {
            var builder = new TransitionBuilder(mockBuilder.Object, new Transition());
            var result = builder.SetNext("");
            Assert.That(result.Equals("mock"));
        }
    }
}
