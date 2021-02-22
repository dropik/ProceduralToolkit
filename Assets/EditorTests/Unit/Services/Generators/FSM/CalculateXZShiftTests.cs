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
            return new CalculateXZShift(context);
        }

        [Test]
        public void TestShiftCalculated()
        {
            var tilingContext = Context.DiamondTilingContext;
            Context.Column = 1;
            tilingContext.First = Vector3.zero;
            ReturnVertex.MoveNext(InputVertices[0]);
            var expectedShift = new Vector3(-1, 0, 0) / 2;
            Assert.That(tilingContext.XZShift, Is.EqualTo(expectedShift));
        }

        [Test]
        public void TestShiftIsNotCalculatedWhenItIsNotZero()
        {
            var tilingContext = Context.DiamondTilingContext;
            Context.Column = 1;
            tilingContext.First = Vector3.zero;
            var expectedShift = new Vector3(4, 4, 4);
            tilingContext.XZShift = expectedShift;
            ReturnVertex.MoveNext(InputVertices[1]);
            Assert.That(tilingContext.XZShift, Is.EqualTo(expectedShift));
        }
    }
}