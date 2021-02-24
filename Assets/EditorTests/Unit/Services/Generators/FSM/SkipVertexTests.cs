using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class SkipVertexTests : BaseStateDecoratorTests
    {
        protected override BaseStateDecorator CreateDecorator(IStateBehaviour wrappee, FSMSettings settings)
        {
            return new SkipVertex(wrappee, settings);
        }

        [Test]
        public void TestMoveNextReturnsEmpty()
        {
            var expectedVertices = new Vector3[0];
            CollectionAssert.AreEqual(expectedVertices, StateDecorator.MoveNext(Vector3.zero));
        }
    }
}