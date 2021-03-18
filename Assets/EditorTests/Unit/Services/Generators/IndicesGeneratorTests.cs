using NUnit.Framework;
using ProceduralToolkit.Models;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    class IndicesGeneratorTests
    {
        [Test]
        public void TestGeneratedIndicesForZeroIterations()
        {
            var context = new LandscapeContext { Iterations = 0, Length = 2 };
            var expectedIndices = new int[]
            {
                0, 1, 2,
                1, 3, 2
            };
            var generator = new IndicesGenerator(context);

            generator.Execute();

            CollectionAssert.AreEqual(expectedIndices, context.Indices);
        }

        [Test]
        public void TestGeneratedIndicesForSecondIteration()
        {
            var context = new LandscapeContext { Iterations = 2, Length = 5 };
            var expectedIndices = new int[]
            {
                0, 1, 5,
                1, 6, 5,
                1, 2, 6,
                2, 7, 6,
                2, 3, 7,
                3, 8, 7,
                3, 4, 8,
                4, 9, 8,

                5, 6, 10,
                6, 11, 10,
                6, 7, 11,
                7, 12, 11,
                7, 8, 12,
                8, 13, 12,
                8, 9, 13,
                9, 14, 13,

                10, 11, 15,
                11, 16, 15,
                11, 12, 16,
                12, 17, 16,
                12, 13, 17,
                13, 18, 17,
                13, 14, 18,
                14, 19, 18,

                15, 16, 20,
                16, 21, 20,
                16, 17, 21,
                17, 22, 21,
                17, 18, 22,
                18, 23, 22,
                18, 19, 23,
                19, 24, 23
            };
            var generator = new IndicesGenerator(context);

            generator.Execute();

            CollectionAssert.AreEqual(expectedIndices, context.Indices);
        }
    }
}
