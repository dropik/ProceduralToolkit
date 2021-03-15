using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class DiamondDsaStep : BaseDsaStep
    {
        public DiamondDsaStep(LandscapeContext context, IDisplacer displacer)
            : base(context, displacer) { }

        public override void Execute(int iteration)
        {
            var context = new DsaStepContext(Length, iteration);
            CalculateDiamonds(context);
        }

        private void CalculateDiamonds(DsaStepContext context)
        {
            foreach (var (row, column) in GetRowsAndColumns(context))
            {
                Vertices[GetIndex(row, column)] = GetDiamond(context, row, column);
            }
        }

        private IEnumerable<(int row, int column)> GetRowsAndColumns(DsaStepContext context)
        {
            for (int row = context.HalfStep; row < Length; row += context.Step)
            {
                for (int column = context.HalfStep; column < Length; column += context.Step)
                {
                    yield return (row, column);
                }
            }
        }

        private Vector3 GetDiamond(DsaStepContext context, int row, int column)
        {
            var diamond = GetVertexOnGrid(row, column);
            diamond.y = GetNeighboursHeightAverage(row, column, context.HalfStep);
            diamond.y += Displacement(context);
            return diamond;
        }

        private Vector3 GetVertexOnGrid(int row, int column)
            => Vector3.Scale(new Vector3(1, 0, 1), Vertices[0] + GetShift(row, column));

        private Vector3 GetShift(int row, int column)
            => Vector3.Scale(new Vector3(column, 0, row), GridSize);

        private float GetNeighboursHeightAverage(int row, int column, int step)
        {
            return (GetUpperLeftNeighbour(row, column, step).y +
                    GetUpperRightNeighbour(row, column, step).y +
                    GetLowerLeftNeighbour(row, column, step).y +
                    GetLowerRightNeighbour(row, column, step).y)
                    /
                    4f;
        }

        private Vector3 GetUpperLeftNeighbour(int row, int column, int step)
            => Vertices[GetIndex(row - step, column - step)];

        private Vector3 GetUpperRightNeighbour(int row, int column, int step)
            => Vertices[GetIndex(row - step, column + step)];

        private Vector3 GetLowerLeftNeighbour(int row, int column, int step)
            => Vertices[GetIndex(row + step, column - step)];

        private Vector3 GetLowerRightNeighbour(int row, int column, int step)
            => Vertices[GetIndex(row + step, column + step)];

        private int GetIndex(int row, int column)
            => row * Length + column;

        private float Displacement(DsaStepContext context)
            => Displacer.GetDisplacement(context.Iteration);
    }
}