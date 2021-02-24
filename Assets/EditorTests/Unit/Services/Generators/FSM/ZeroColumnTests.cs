using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ZeroColumnTests : BaseStateDecoratorTests
    {
        protected override BaseStateDecorator CreateDecorator(IStateBehaviour state, FSMSettings settings)
        {
            return new ZeroColumn(state, settings);
        }

        [Test]
        public void TestColumnZeroed()
        {
            Settings.FSMContext.Column = 1;
            StateDecorator.MoveNext(default);
            Assert.That(Settings.FSMContext.Column, Is.Zero);
        }
    }
}
