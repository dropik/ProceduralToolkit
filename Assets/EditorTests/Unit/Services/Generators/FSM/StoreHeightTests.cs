using NUnit.Framework;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators.FSM
{
    [Category("Unit")]
    public class StoreHeightTests
    {
        [Test]
        public void TestHeightStored()
        {
            var expectedHeight = 10f;
            var input = new Vector3(1, expectedHeight, 3);
            var context = new AdderContext(3)
            {
                Column = 2
            };
            var storeHeight = new StoreHeight(context);

            storeHeight.Process(input);

            Assert.That(context.Heights[context.Column], Is.EqualTo(expectedHeight));
        }
    }
}
