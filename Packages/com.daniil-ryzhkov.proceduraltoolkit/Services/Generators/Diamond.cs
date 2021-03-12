using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond : IDsaStep
    {
        private readonly IDisplacer displacer;

        public Diamond(IDisplacer displacer)
        {
            this.displacer = displacer;
        }

        public void Execute(Vector3[] vertices, int iteration)
        {
            var context = new DiamondContext(vertices, iteration);
            CalculateDiamonds(context);
        }

        private void CalculateDiamonds(DiamondContext context)
        {
            foreach (var (row, column) in GetRowsAndColumns(context))
            {
                context.Vertices[GetIndex(context, row, column)] = GetDiamond(context, row, column);
            }
        }

        private IEnumerable<(int row, int column)> GetRowsAndColumns(DiamondContext context)
        {
            for (int row = context.Step; row < context.Length; row += context.DiamondStep)
            {
                for (int column = context.Step; column < context.Length; column += context.DiamondStep)
                {
                    yield return (row, column);
                }
            }
        }

        private Vector3 GetDiamond(DiamondContext context, int row, int column)
        {
            var diamond = GetSumOfNeighbours(context, row, column);
            diamond += context.XzShift;
            diamond.y += Displacement(context);
            return diamond;
        }

        private Vector3 GetSumOfNeighbours(DiamondContext context, int row, int column)
        {
            var sum = GetUpperLeftNeighbour(context, row, column);
            sum.y += GetUpperRightNeighbour(context, row, column).y;
            sum.y += GetLowerLeftNeighbour(context, row, column).y;
            sum.y += GetLowerRightNeighbour(context, row, column).y;
            sum.y /= 4f;
            return sum;
        }

        private Vector3 GetUpperLeftNeighbour(DiamondContext context, int row, int column)
            => context.Vertices[GetIndex(context, row - context.Step, column - context.Step)];

        private Vector3 GetUpperRightNeighbour(DiamondContext context, int row, int column)
            => context.Vertices[GetIndex(context, row - context.Step, column + context.Step)];

        private Vector3 GetLowerLeftNeighbour(DiamondContext context, int row, int column)
            => context.Vertices[GetIndex(context, row + context.Step, column - context.Step)];

        private Vector3 GetLowerRightNeighbour(DiamondContext context, int row, int column)
            => context.Vertices[GetIndex(context, row + context.Step, column + context.Step)];

        private int GetIndex(DiamondContext context, int row, int column)
            => row * context.Length + column;

        private float Displacement(DiamondContext context)
            => displacer.GetDisplacement(context.Iteration);
    }
}