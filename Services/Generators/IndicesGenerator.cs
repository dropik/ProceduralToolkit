using ProceduralToolkit.Models;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public class IndicesGenerator : IIndicesGenerator
    {
        private readonly LandscapeContext context;

        public IndicesGenerator(LandscapeContext context)
        {
            this.context = context;
        }

        public void Execute()
        {
            var indices = AllocateBuffer();
            SetIndices(indices);
            context.Indices = indices;
        }

        private int[] AllocateBuffer()
        {
            var squares = (context.Length - 1) * (context.Length - 1);
            return new int[squares * 2 * 3];
        }

        private void SetIndices(int[] indices)
        {
            foreach (var (row, column, index) in IdTuples)
            {
                SetIndicesForOneSquare(indices, row, column, index);
            }
        }

        private IEnumerable<(int row, int column, int index)> IdTuples
        {
            get
            {
                var index = 0;
                for (int row = 0; row < context.Length - 1; row++)
                {
                    for (int column = 0; column < context.Length - 1; column++)
                    {
                        yield return (row, column, index);
                        index += 6;
                    }
                }
            }
        }

        private void SetIndicesForOneSquare(int[] indices, int row, int column, int index)
        {
            var upLeft = row * context.Length + column;
            var upRight = row * context.Length + column + 1;
            var downLeft = (row + 1) * context.Length + column;
            var downRight = (row + 1) * context.Length + column + 1;

            indices[index] = upLeft;
            indices[index + 1] = upRight;
            indices[index + 2] = downLeft;
            indices[index + 3] = upRight;
            indices[index + 4] = downRight;
            indices[index + 5] = downLeft;
        }
    }
}
