using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class CalculateXZShiftTests : ReturnDiamondTests
    {
        protected override BaseDiamondTilingState GetReturnVertex(DiamondTilingContext context)
        {
            return new CalculateXZShift(context);
        }

        [Test]
        public void TestShiftCalculated()
        {
            Context.Column = 1;
            Context.First = Vector3.zero;
            ReturnVertex.MoveNext(InputVertices[1]);
            var expectedShift = new Vector3(-2, 0, 0) / 2;
            Assert.That(Context.XZShift, Is.EqualTo(expectedShift));
        }

        [Test]
        public void TestShiftIsNotCalculatedWhenItIsNotZero()
        {
            Context.Column = 1;
            Context.First = Vector3.zero;
            var expectedShift = new Vector3(4, 4, 4);
            Context.XZShift = expectedShift;
            ReturnVertex.MoveNext(InputVertices[1]);
            Assert.That(Context.XZShift, Is.EqualTo(expectedShift));
        }
    }
}