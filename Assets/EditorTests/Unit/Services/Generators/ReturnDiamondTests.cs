﻿using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnDiamondTests : BaseDiamondTilingStateTests
    {
        protected override BaseDiamondTilingState GetReturnVertex(DiamondTilingContext context)
        {
            return new ReturnDiamond(context);
        }

        [Test]
        public void TestMoveNextReturnsDiamond()
        {
            Context.XZShift = new Vector3(0.5f, 0, 0.5f);
            var expectedVertex = new Vector3(1.5f, 0, 0.5f);
            Assert.That(ReturnVertex.MoveNext(InputVertices[0]), Is.EqualTo(expectedVertex));
        }
    }
}
