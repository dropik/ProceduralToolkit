using NUnit.Framework;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class SquaresMaskTests
    {
        [Test]
        [TestCase(3, new bool[]
        {
            false, true, false,
            true, false, true,
            false, true, false
        })]
        [TestCase(5, new bool[]
            {
                false, true, false, true, false,

                true, false, true, false, true,

                false, true, false, true, false,

                true, false, true, false, true,

                false, true, false, true, false
            })]
        public void TestMask(int length, bool[] expectedMask)
        {
            var mask = new SquaresMask()
            {
                Length = length
            };

            CollectionAssert.AreEqual(expectedMask, mask.Mask);
        }
    }
}
