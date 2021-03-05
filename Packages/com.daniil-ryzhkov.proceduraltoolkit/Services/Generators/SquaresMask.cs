using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public class SquaresMask
    {
        public int Length { get; set; }

        public IEnumerable<bool> Mask
        {
            get
            {
                var limit = Length * Length;
                for (int i = 0; i < limit - 1; i += 2)
                {
                    yield return false;
                    yield return true;
                }
                yield return false;
            }
        }
    }
}
