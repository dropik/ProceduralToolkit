using ProceduralToolkit.Models;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public class DiamondDsaStep : BaseDsaStep
    {
        public DiamondDsaStep(LandscapeContext context, IDisplacer displacer)
            : base(context, displacer) { }

        protected override IEnumerable<(int row, int column)> GetRowsAndColumnsForStep(DsaStepContext context)
        {
            for (int row = context.HalfStep; row < Length; row += context.Step)
            {
                for (int column = context.HalfStep; column < Length; column += context.Step)
                {
                    yield return (row, column);
                }
            }
        }

        protected override float GetNeighboursHeightAverage(int row, int column, int step)
        {
            return (GetUpLeftHeight(row, column, step) +
                    GetUpRightHeight(row, column, step) +
                    GetDownLeftHeight(row, column, step) +
                    GetDownRightHeight(row, column, step))
                    /
                    4f;
        }

        private float GetUpLeftHeight(int row, int column, int step)
            => Vertices[GetIndex(row - step, column - step)].y;

        private float GetUpRightHeight(int row, int column, int step)
            => Vertices[GetIndex(row - step, column + step)].y;

        private float GetDownLeftHeight(int row, int column, int step)
            => Vertices[GetIndex(row + step, column - step)].y;

        private float GetDownRightHeight(int row, int column, int step)
            => Vertices[GetIndex(row + step, column + step)].y;
    }
}