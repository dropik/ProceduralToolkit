using ProceduralToolkit.Models;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class DiamondStep : BaseAlgorithmStep
    {
        public DiamondStep(LandscapeContext context, IDisplacer displacer)
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