﻿using ProceduralToolkit.Models;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class SquareStep : BaseAlgorithmStep
    {
        public SquareStep(LandscapeContext context, IDisplacer displacer)
            : base(context, displacer) { }

        protected override IEnumerable<(int row, int column)> GetRowsAndColumnsForStep(DsaStepContext context)
        {
            var start = context.HalfStep;
            for (int row = 0; row < Context.Length; row += context.HalfStep)
            {
                for (int column = start; column < Context.Length; column += context.Step)
                {
                    yield return (row, column);
                }
                start = (start + context.HalfStep) % context.Step;
            }
        }

        protected override float GetNeighboursHeightAverage(int row, int column, int halfStep)
        {
            return (GetUpHeight(row, column, halfStep) +
                    GetRightHeight(row, column, halfStep) +
                    GetDownHeight(row, column, halfStep) +
                    GetLeftHeight(row, column, halfStep))
                    /
                    4f;
        }

        private float GetUpHeight(int row, int column, int step)
        {
            row = ShiftBackward(row, step);
            return Context.Heights[row, column];
        }

        private float GetRightHeight(int row, int column, int step)
        {
            column = ShiftForward(column, step);
            return Context.Heights[row, column];
        }

        private float GetDownHeight(int row, int column, int step)
        {
            row = ShiftForward(row, step);
            return Context.Heights[row, column];
        }

        private float GetLeftHeight(int row, int column, int step)
        {
            column = ShiftBackward(column, step);
            return Context.Heights[row, column];
        }

        private int ShiftForward(int index, int step)
        {
            index += step;
            if (index > Context.Length - 1)
            {
                index -= Context.Length - 1;
            }
            return index;
        }

        private int ShiftBackward(int index, int step)
        {
            index -= step;
            if (index < 0)
            {
                index += Context.Length - 1;
            }
            return index;
        }
    }
}
