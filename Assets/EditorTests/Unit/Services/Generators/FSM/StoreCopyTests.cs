using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StoreCopyTests
    {
        [Test]
        public void TestVertexCopyStored()
        {
            var context = new RowDuplicatorContext(2);
            var expectedVertex = new Vector3(1, 2, 3);
            var processor = new StoreCopy(context);

            processor.Process(expectedVertex);

            Assert.That(context.VerticesCopies[0], Is.EqualTo(expectedVertex));
        }
    }
}
