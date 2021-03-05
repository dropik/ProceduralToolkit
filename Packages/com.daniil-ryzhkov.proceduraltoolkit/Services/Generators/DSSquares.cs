using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public class DSSquares
    {
        public int Iterations { get; set; }
        public IEnumerable<Models.Square> Squares
        {
            get
            {
                var length = new IterationToLength { Iteration = Iterations }.Length;
                for (int i = 0; i < length - 1; i++)
                {
                    for (int j = 0; j < length - 1; j++)
                    {
                        var upLeft = i * length + j;
                        yield return new Models.Square(upLeft, upLeft + 1, upLeft + length + 1, upLeft + length);
                    }
                }
            }
        }
    }
}
