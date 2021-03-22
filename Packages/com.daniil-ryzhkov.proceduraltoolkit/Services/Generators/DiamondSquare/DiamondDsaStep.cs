using ProceduralToolkit.Models;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class DiamondDsaStep : BaseDsaStep
    {
        public DiamondDsaStep(LandscapeContext context, IDisplacer displacer)
            : base(context, displacer) { }

        protected override IEnumerable<(int row, int column)> GetRowsAndColumnsForStep(DsaStepContext context)
        {
            for (int row = context.HalfStep; row < Context.Length; row += context.Step)
            {
                for (int column = context.HalfStep; column < Context.Length; column += context.Step)
                {
                    yield return (row, column);
                }
            }
        }

        protected override float GetNeighboursAverage(int row, int column, int step)
        {
            return (GetUpLeft(row, column, step) +
                    GetUpRight(row, column, step) +
                    GetDownLeft(row, column, step) +
                    GetDownRight(row, column, step))
                    /
                    4f;
        }

        private float GetUpLeft(int row, int column, int step)
            => Context.Vertices[GetIndex(row - step, column - step)].y;

        private float GetUpRight(int row, int column, int step)
            => Context.Vertices[GetIndex(row - step, column + step)].y;

        private float GetDownLeft(int row, int column, int step)
            => Context.Vertices[GetIndex(row + step, column - step)].y;

        private float GetDownRight(int row, int column, int step)
            => Context.Vertices[GetIndex(row + step, column + step)].y;

        protected override float GetNeighboursHeightAverage(int row, int column, int halfStep)
        {
            return (GetUpLeftHeight(row, column, halfStep) +
                    GetUpRightHeight(row, column, halfStep) +
                    GetDownLeftHeight(row, column, halfStep) +
                    GetDownRightHeight(row, column, halfStep))
                    /
                    4f;
        }

        private float GetUpLeftHeight(int row, int column, int step)
            => Context.Heights[row - step, column - step];

        private float GetUpRightHeight(int row, int column, int step)
            => Context.Heights[row - step, column + step];

        private float GetDownLeftHeight(int row, int column, int step)
            => Context.Heights[row + step, column - step];

        private float GetDownRightHeight(int row, int column, int step)
            => Context.Heights[row + step, column + step];
    }
}