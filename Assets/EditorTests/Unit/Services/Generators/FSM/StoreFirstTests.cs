using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StoreFirstTests
    {
        [Test]
        public void TestFirstStored()
        {
            var context = new DiamondTilingContext();
            var expectedVertex = new Vector3(1, 2, 3);
            var processor = new StoreFirst(context);
            processor.Process(expectedVertex);
            Assert.That(context.First, Is.EqualTo(expectedVertex));
        }
    }
}
