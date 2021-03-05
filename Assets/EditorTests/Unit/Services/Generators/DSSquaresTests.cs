using NUnit.Framework;
using ProceduralToolkit.Services.Generators;
using System.Collections.Generic;

namespace ProceduralToolkit.EditorTests.Unit.Services.Generators
{
    [Category("Unit")]
    public class DSSquaresTests
    {
        private static IEnumerable<TestCaseData> SquaresTestCases
        {
            get
            {
                yield return new TestCaseData(0, new Models.Square[]
                {
                    new Models.Square(0, 1, 3, 2)
                }).SetName("Iterations: 0");

                yield return new TestCaseData(2, new Models.Square[]
                {
                    new Models.Square(0, 1, 6, 5),
                    new Models.Square(1, 2, 7, 6),
                    new Models.Square(2, 3, 8, 7),
                    new Models.Square(3, 4, 9, 8),
                    new Models.Square(5, 6, 11, 10),
                    new Models.Square(6, 7, 12, 11),
                    new Models.Square(7, 8, 13, 12),
                    new Models.Square(8, 9, 14, 13),
                    new Models.Square(10, 11, 16, 15),
                    new Models.Square(11, 12, 17, 16),
                    new Models.Square(12, 13, 18, 17),
                    new Models.Square(13, 14, 19, 18),
                    new Models.Square(15, 16, 21, 20),
                    new Models.Square(16, 17, 22, 21),
                    new Models.Square(17, 18, 23, 22),
                    new Models.Square(18, 19, 24, 23)
                }).SetName("Iterations: 2");
            }
        }

        [Test]
        [TestCaseSource("SquaresTestCases")]
        public void TestGeneratedSquares(int iterations, IEnumerable<Models.Square> expectedSquares)
        {
            var dsSquares = new DSSquares { Iterations = iterations };
            CollectionAssert.AreEqual(expectedSquares, dsSquares.Squares);
        }
    }
}
