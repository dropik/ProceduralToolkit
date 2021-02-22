using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ReturnOriginalTests : BaseDiamondTilingStateTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            return new FSMContext(columns);
        }

        protected override BaseDiamondTilingState GetReturnVertex(FSMSettings settings)
        {
            return new ReturnOriginal(settings);
        }

        [Test]
        public void TestMoveNextReturnsCurrent()
        {
            var expectedVertices = new Vector3[] { InputVertices[0] };
            CollectionAssert.AreEqual(expectedVertices, ReturnVertex.MoveNext(InputVertices[0]));
        }
    }
}