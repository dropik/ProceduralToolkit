using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class SquareDsaStep : BaseDsaStep
    {
        public SquareDsaStep(LandscapeContext context, IDisplacer displacer)
            : base(context, displacer) { }

        protected override IEnumerable<(int row, int column)> GetRowsAndColumnsForStep(DsaStepContext context)
        {
            var start = context.HalfStep;
            for (int row = 0; row < Length; row += context.HalfStep)
            {
                for (int column = start; column < Length; column += context.Step)
                {
                    yield return (row, column);
                }
                start = (start + context.HalfStep) % context.Step;
            }
        }

        protected override Vector3 GetStepVertex(int row, int column, DsaStepContext context)
        {
            var square = GetVertexOnGrid(row, column);
            square.y = GetNeighboursHeightAverage(row, column, context.HalfStep);
            square.y += Displacer.GetDisplacement(context.Iteration);
            return square;
        }

        private Vector3 GetVertexOnGrid(int row, int column)
            => Vector3.Scale(new Vector3(1, 0, 1), Vertices[0] + GetShift(row, column));

        private Vector3 GetShift(int row, int column)
            => Vector3.Scale(new Vector3(column, 0, row), GridSize);

        private float GetNeighboursHeightAverage(int row, int column, int step)
        {
            return (UpNeighbour(row, column, step) +
                    RightNeighbour(row, column, step) +
                    DownNeighbour(row, column, step) +
                    LeftNeighbour(row, column, step))
                    /
                    4f;
        }

        private float UpNeighbour(int row, int column, int step)
        {
            row = ShiftBackward(row, step);
            return Vertices[GetIndex(row, column)].y;
        }

        private float RightNeighbour(int row, int column, int step)
        {
            column = ShiftForward(column, step);
            return Vertices[GetIndex(row, column)].y;
        }

        private float DownNeighbour(int row, int column, int step)
        {
            row = ShiftForward(row, step);
            return Vertices[GetIndex(row, column)].y;
        }

        private float LeftNeighbour(int row, int column, int step)
        {
            column = ShiftBackward(column, step);
            return Vertices[GetIndex(row, column)].y;
        }

        private int ShiftForward(int index, int step)
        {
            index += step;
            if (index > Length - 1)
            {
                index -= Length - 1;
            }
            return index;
        }

        private int ShiftBackward(int index, int step)
        {
            index -= step;
            if (index < 0)
            {
                index += Length - 1;
            }
            return index;
        }
    }
}
