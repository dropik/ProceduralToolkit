using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class EnqueueVertexTests
    {
        [Test]
        public void TestVertexEnqueued()
        {
            var input = new Vector3(1, 2, 3);
            var context = new InvertorContext(3);
            var enqueueVertex = new EnqueueVertex(context);

            enqueueVertex.Process(input);
            var result = context.Queue.Dequeue();
            Assert.That(result, Is.EqualTo(input));
        }
    }
}
