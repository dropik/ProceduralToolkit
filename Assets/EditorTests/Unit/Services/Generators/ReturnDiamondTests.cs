using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnDiamondTests : BaseReturnVertexTests
    {
        protected override BaseReturnVertex GetReturnVertex(DiamondContext context)
        {
            return new ReturnDiamond(context);
        }

        [Test]
        public void TestMoveNextReturnsDiamond()
        {
            Context.XZShift = new Vector3(0.5f, 0, 0.5f);
            var expectedVertex = InputVertices[0] + Context.XZShift;
            Assert.That(ReturnVertex.MoveNext(InputVertices[0]), Is.EqualTo(expectedVertex));
        }
    }
}
