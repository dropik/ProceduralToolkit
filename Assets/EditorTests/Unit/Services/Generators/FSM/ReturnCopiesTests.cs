using NUnit.Framework;
using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;
using System.Linq;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class ReturnCopiesTests : BaseStateTests
    {
        protected override FSMContext CreateContext(int columns)
        {
            return new FSMContext(columns)
            {
                RowDuplicatorContext = new RowDuplicatorContext(columns)
            };
        }

        protected override BaseState GetReturnVertex(FSMSettings settings)
        {
            return new ReturnCopies(settings);
        }

        [Test]
        public void TestVertexCopyStored()
        {
            ReturnVertex.MoveNext(InputVertices[0]);
            Assert.That(Settings.FSMContext.RowDuplicatorContext.VerticesCopies[0], Is.EqualTo(InputVertices[0]));
        }

        [Test]
        public void TestReturnedVertices()
        {
            var context = Settings.FSMContext;
            context.Column = 1;
            for (int i = 0; i < context.ColumnsInRow; i++)
            {
                context.RowDuplicatorContext.VerticesCopies[i] = new Vector3(0, 0, i);
            }
            var expectedVertices = new Vector3[] { InputVertices[1] }.Concat(context.RowDuplicatorContext.VerticesCopies);

            CollectionAssert.AreEqual(expectedVertices, ReturnVertex.MoveNext(InputVertices[1]));
        }
    }
}
