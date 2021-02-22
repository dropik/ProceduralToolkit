using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class CalculateXZShiftTests : ReturnDiamondTests
    {
        protected override BaseState GetReturnVertex(FSMSettings settings)
        {
            return new CalculateXZShift(settings);
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
            var expectedShift = new Vector3(-1, 0, 0) / 2;

            ReturnVertex.MoveNext(InputVertices[0]);

            Assert.That(Settings.FSMContext.DiamondTilingContext.XZShift, Is.EqualTo(expectedShift));
        }

        [Test]
        public void TestShiftIsNotCalculatedWhenItIsNotZero()
        {
            SetupAsForSecondVertex();
            var context = Settings.FSMContext;
            var expectedShift = new Vector3(4, 4, 4);
            context.DiamondTilingContext.XZShift = expectedShift;

            ReturnVertex.MoveNext(InputVertices[1]);

            Assert.That(context.DiamondTilingContext.XZShift, Is.EqualTo(expectedShift));
        }
    }
}