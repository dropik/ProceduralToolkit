using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class CalculateXZShiftTests : BaseStateDecoratorTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            var context = base.CreateContext(columns);
            context.DiamondTilingContext = new DiamondTilingContext();
            return context;
        }

        protected override BaseStateDecorator CreateDecorator(IStateBehaviour wrappee, FSMSettings settings)
        {
            return new CalculateXZShift(wrappee, settings);
        }

        private void SetupAsForSecondVertex()
        {
            var context = Settings.FSMContext;
            context.Column = 1;
            context.DiamondTilingContext.First = Vector3.zero;
        }

        [Test]
        public void TestShiftCalculated()
        {
            SetupAsForSecondVertex();
            var input = new Vector3(1, 0, 0);
            var expectedShift = new Vector3(-1, 0, 0) / 2;

            StateDecorator.MoveNext(input);

            Assert.That(Settings.FSMContext.DiamondTilingContext.XZShift, Is.EqualTo(expectedShift));
        }

        [Test]
        public void TestShiftIsNotCalculatedWhenItIsNotZero()
        {
            SetupAsForSecondVertex();
            var context = Settings.FSMContext;
            var expectedShift = new Vector3(4, 4, 4);
            context.DiamondTilingContext.XZShift = expectedShift;

            StateDecorator.MoveNext(Vector3.zero);

            Assert.That(context.DiamondTilingContext.XZShift, Is.EqualTo(expectedShift));
        }
    }
}