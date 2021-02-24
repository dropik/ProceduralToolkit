using Moq;
using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class TransitionBuilderTests
    {
        private Mock<IState> mockState;

        [SetUp]
        public void Setup()
        {
            mockState = new Mock<IState>();
            mockState.Setup(m => m.Equals(It.Is<string>(s => s == "mock"))).Returns(true);
        }

        [Test]
        public void TestNextSetIsSet()
        {
            var transition = new Transition();
            var builder = new TransitionBuilder(null, transition);

            builder.SetNext(mockState.Object);

            Assert.That(transition.NextState.Equals("mock"));
        }

        [Test]
        public void TestGivenStateReturned()
        {
            var builder = new TransitionBuilder(mockState.Object, new Transition());
            var result = builder.SetNext(null);
            Assert.That(result.Equals("mock"));
        }
    }
}
