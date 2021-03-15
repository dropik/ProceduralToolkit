using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class DiamondDsaStep : IDsaStep
    {
        private readonly Vector3[] vertices;
        private readonly int length;
        private readonly Vector3 gridSize;
        private readonly IDisplacer displacer;

        public DiamondDsaStep(Vector3[] vertices, int length, Vector3 gridSize, IDisplacer displacer)
        {
            this.vertices = vertices;
            this.length = length;
            this.gridSize = gridSize;
            this.displacer = displacer;
        }

        public void Execute(int iteration)
        {
            var context = new DsaStepContext(length, iteration);
            CalculateDiamonds(context);
        }

        private void CalculateDiamonds(DsaStepContext context)
        {
            foreach (var (row, column) in GetRowsAndColumns(context))
            {
                vertices[GetIndex(row, column)] = GetDiamond(context, row, column);
            }
        }

        private IEnumerable<(int row, int column)> GetRowsAndColumns(DsaStepContext context)
        {
            for (int row = context.HalfStep; row < length; row += context.Step)
            {
                for (int column = context.HalfStep; column < length; column += context.Step)
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
            => Vector3.Scale(new Vector3(1, 0, 1), vertices[0] + GetShift(row, column));

        private Vector3 GetShift(int row, int column)
            => Vector3.Scale(new Vector3(column, 0, row), gridSize);

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
            => vertices[GetIndex(row - step, column - step)];

        private Vector3 GetUpperRightNeighbour(int row, int column, int step)
            => vertices[GetIndex(row - step, column + step)];

        private Vector3 GetLowerLeftNeighbour(int row, int column, int step)
            => vertices[GetIndex(row + step, column - step)];

        private Vector3 GetLowerRightNeighbour(int row, int column, int step)
            => vertices[GetIndex(row + step, column + step)];

        private int GetIndex(int row, int column)
            => row * length + column;

        private float Displacement(DsaStepContext context)
            => displacer.GetDisplacement(context.Iteration);
    }
}