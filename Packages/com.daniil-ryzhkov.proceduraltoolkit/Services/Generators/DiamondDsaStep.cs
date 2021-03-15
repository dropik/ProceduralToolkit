using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class DiamondDsaStep : IDsaStep
    {
        private readonly Vector3[] vertices;
        private readonly int length;
        private readonly IDisplacer displacer;

        public DiamondDsaStep(Vector3[] vertices, int length, IDisplacer displacer)
        {
            this.vertices = vertices;
            this.length = length;
            this.displacer = displacer;
        }

        public void Execute(int iteration)
        {
            var context = new DiamondContext(vertices, length, iteration);
            CalculateDiamonds(context);
        }

        private void CalculateDiamonds(DiamondContext context)
        {
            foreach (var (row, column) in GetRowsAndColumns(context))
            {
                vertices[GetIndex(row, column)] = GetDiamond(context, row, column);
            }
        }

        private IEnumerable<(int row, int column)> GetRowsAndColumns(DiamondContext context)
        {
            for (int row = context.Step; row < length; row += context.DiamondStep)
            {
                for (int column = context.Step; column < length; column += context.DiamondStep)
                {
                    yield return (row, column);
                }
            }
        }

        private Vector3 GetDiamond(DiamondContext context, int row, int column)
        {
            var diamond = GetSumOfNeighbours(row, column, context.Step);
            diamond += context.XzShift;
            diamond.y += Displacement(context);
            return diamond;
        }

        private Vector3 GetSumOfNeighbours(int row, int column, int step)
        {
            var sum = GetUpperLeftNeighbour(row, column, step);
            sum.y += GetUpperRightNeighbour(row, column, step).y;
            sum.y += GetLowerLeftNeighbour(row, column, step).y;
            sum.y += GetLowerRightNeighbour(row, column, step).y;
            sum.y /= 4f;
            return sum;
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

        private float Displacement(DiamondContext context)
            => displacer.GetDisplacement(context.Iteration);
    }
}