using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnDiamondTests : BaseReturnVertexTests
    {
        protected override BaseReturnVertex GetReturnVertex(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
        {
            return new ReturnDiamond(inputVerticesEnumerator, context);
        }

        [Test]
        public void TestOriginalVertexIsStored()
        {
            ReturnVertex.MoveNext();
            Assert.That(Context.OriginalVertices[0], Is.EqualTo(InputVertices[0]));
        }
        
        [Test]
        public void TestDiamondCalculatedCorrectly()
        {
            var expectedVertex = InputVertices[0] + Context.XZShift;
            ReturnVertex.MoveNext();
            Assert.That(Context.Current, Is.EqualTo(expectedVertex));
        }

        protected override BaseReturnVertex GetReturnVertex(DiamondContext context)
        {
            throw new System.NotImplementedException();
        }
    }
}
