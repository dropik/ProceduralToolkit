using System.Collections.Generic;
using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class ReturnOriginalTests : BaseReturnVertexTests
    {
        protected override BaseReturnVertex GetReturnVertex(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
        {
            return new ReturnOriginal(inputVerticesEnumerator, context);
        }

        [Test]
        public void TestCurrentSetAsGivenVertex()
        {
            ReturnVertex.MoveNext();
            Assert.That(Context.Current, Is.EqualTo(InputVertices[0]));
        }
    }
}