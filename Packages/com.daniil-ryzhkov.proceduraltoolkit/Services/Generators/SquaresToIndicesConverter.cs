using ProceduralToolkit.Models;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public class SquaresToIndicesConverter
    {
        private readonly IEnumerable<Square> squares;

        public SquaresToIndicesConverter(IEnumerable<Square> squares)
        {
            this.squares = squares;
        }

        public IEnumerable<int> Indices
        {
            get
            {
                foreach (var square in squares)
                {
                    yield return square.Index1;
                    yield return square.Index2;
                    yield return square.Index3;
                    yield return square.Index1;
                    yield return square.Index3;
                    yield return square.Index4;
                }
            }
        }
    }
}
