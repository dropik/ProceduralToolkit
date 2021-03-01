using NUnit.Framework;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DiamondsMaskTests
    {
        [Test]
        [TestCase(2, new bool[]
        {
            false, false,
            true,
            false, false
        })]
        [TestCase(5, new bool[]
            {
                false, false, false, false, false,
                true, true, true, true,

                false, false, false, false, false,
                true, true, true, true,

                false, false, false, false, false,
                true, true, true, true,

                false, false, false, false, false,
                true, true, true, true,

                false, false, false, false, false
            })]
        public void TestMask(int length, bool[] expectedVertices)
        {
            var mask = new DiamondsMask()
            {
                Length = length
            };

            CollectionAssert.AreEqual(expectedVertices, mask.Mask);
        }
    }
}
