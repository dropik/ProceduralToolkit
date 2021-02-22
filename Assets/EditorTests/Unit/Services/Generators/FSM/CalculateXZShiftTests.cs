using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class CalculateXZShiftTests : ReturnDiamondTests
    {
        protected override BaseDiamondTilingState GetReturnVertex(FSMContext context)
        {
            return new CalculateXZShift(context as DiamondTilingContext);
        }

        [Test]
        public void TestShiftCalculated()
        {
            var context = Context as DiamondTilingContext;
            context.Column = 1;
            context.First = Vector3.zero;
            ReturnVertex.MoveNext(InputVertices[0]);
            var expectedShift = new Vector3(-1, 0, 0) / 2;
            Assert.That(context.XZShift, Is.EqualTo(expectedShift));
        }

        [Test]
        public void TestShiftIsNotCalculatedWhenItIsNotZero()
        {
            var context = Context as DiamondTilingContext;
            context.Column = 1;
            context.First = Vector3.zero;
            var expectedShift = new Vector3(4, 4, 4);
            context.XZShift = expectedShift;
            ReturnVertex.MoveNext(InputVertices[1]);
            Assert.That(context.XZShift, Is.EqualTo(expectedShift));
        }
    }
}