using ProceduralToolkit.Models;
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
            for (int row = context.Step; row < context.Length; row += context.DiamondStep)
            {
                for (int column = context.Step; column < context.Length; column += context.DiamondStep)
                {
                    context.Vertices[GetIndex(context, row, column)] = GetDiamond(context, row, column);
                }
            }
        }

        private Vector3 GetDiamond(DiamondContext context, int row, int column)
        {
            var diamond = GetUpperLeftNeighbour(context, row, column);
            diamond.y += GetUpperRightNeighbour(context, row, column).y;
            diamond.y += GetLowerLeftNeighbour(context, row, column).y;
            diamond.y += GetLowerRightNeighbour(context, row, column).y;
            diamond.y /= 4f;

            diamond += context.XzShift;

            diamond.y += Displacement(context);

            return diamond;
        }

        private Vector3 GetUpperLeftNeighbour(DiamondContext context, int row, int column) =>
            context.Vertices[GetIndex(context, row - context.Step, column - context.Step)];

        private Vector3 GetUpperRightNeighbour(DiamondContext context, int row, int column) =>
            context.Vertices[GetIndex(context, row - context.Step, column + context.Step)];

        private Vector3 GetLowerLeftNeighbour(DiamondContext context, int row, int column) =>
            context.Vertices[GetIndex(context, row + context.Step, column - context.Step)];

        private Vector3 GetLowerRightNeighbour(DiamondContext context, int row, int column) =>
            context.Vertices[GetIndex(context, row + context.Step, column + context.Step)];

        private int GetIndex(DiamondContext context, int row, int column) => row * context.Length + column;

        private float Displacement(DiamondContext context) => displacer.GetDisplacement(context.Iteration);
    }
}