using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    public class NewStoreCopyTests : ReturnOriginalTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            var context = base.CreateContext(columns);
            context.RowDuplicatorContext = new RowDuplicatorContext(columns);
            return context;
        }

        protected override BaseState GetReturnVertex(FSMSettings settings)
        {
            return new StoreCopy(settings);
        }

        [Test]
        public void TestVertexCopyStored()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Settings.FSMContext.RowDuplicatorContext.VerticesCopies[0], Is.EqualTo(InputVertices[0]));
        }
    }
}
