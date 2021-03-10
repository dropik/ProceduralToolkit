using System;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly Vector3[] inputVertices;
        private readonly int verticesInRow;
        private readonly Vector3 xzShift;
        private readonly Vector3[] output;

        public Diamond(Vector3[] inputVertices, int iteration)
        {
            this.inputVertices = inputVertices;

            verticesInRow = (int)Mathf.Pow(2, iteration) + 1;

            output = new Vector3[2 * verticesInRow * (verticesInRow - 1) + 1];

            xzShift = (inputVertices[verticesInRow + 1] - inputVertices[0]) / 2f;
            xzShift.y = 0;
        }

        public Vector3[] Output
        {
            get
            {
                var index = 0;
                foreach (var vertex in inputVertices)
                {
                    HandleVertexFromInput(vertex, index);
                    index++;
                }
                return output;
            }
        }

        private void HandleVertexFromInput(Vector3 vertex, int index)
        {
            var (row, column) = IndexToRowColumn(index);
            HandleOriginal(vertex, row, column);
            HandleDiamond(vertex, row, column);
        }

        private (int row, int column) IndexToRowColumn(int index)
        {
            return (index / verticesInRow, index % verticesInRow);
        }

        private void HandleDiamond(Vector3 vertex, int row, int column)
        {
            Action<Vector3, int, int> diamondHandler = InitDiamondHandlerDelegate();
            diamondHandler = FilterDiamondHandler(diamondHandler, row, column);
            diamondHandler(vertex, row, column);
        }

        private Action<Vector3, int, int> InitDiamondHandlerDelegate()
        {
            Action<Vector3, int, int> diamondHandler = HandleUpperLeft;
            diamondHandler += HandleUpperRight;
            diamondHandler += HandleLowerLeft;
            diamondHandler += HandleLowerRight;
            return diamondHandler;
        }

        private Action<Vector3, int, int> FilterDiamondHandler(Action<Vector3, int, int> diamondHandler, int row, int column)
        {
            diamondHandler = FilterByRow(diamondHandler, row);
            diamondHandler = FilterByColumn(diamondHandler, column);
            return diamondHandler;
        }

        private Action<Vector3, int, int> FilterByRow(Action<Vector3, int, int> diamondHandler, int row)
        {
            if (row == 0)                   return ExcludeUpperDiamonds(diamondHandler);
            if (row == verticesInRow - 1)   return ExcludeLowerDiamonds(diamondHandler);

            return diamondHandler;
        }

        private Action<Vector3, int, int> FilterByColumn(Action<Vector3, int, int> diamondHandler, int column)
        {
            if (column == 0)                    return ExcludeLeftDiamonds(diamondHandler);
            if (column == verticesInRow - 1)    return ExcludeRightDiamonds(diamondHandler);

            return diamondHandler;
        }

        private Action<Vector3, int, int> ExcludeUpperDiamonds(Action<Vector3, int, int> diamondHandler)
        {
            diamondHandler -= HandleUpperLeft;
            diamondHandler -= HandleUpperRight;
            return diamondHandler;
        }

        private Action<Vector3, int, int> ExcludeLowerDiamonds(Action<Vector3, int, int> diamondHandler)
        {
            diamondHandler -= HandleLowerLeft;
            diamondHandler -= HandleLowerRight;
            return diamondHandler;
        }

        private Action<Vector3, int, int> ExcludeLeftDiamonds(Action<Vector3, int, int> diamondHandler)
        {
            diamondHandler -= HandleUpperLeft;
            diamondHandler -= HandleLowerLeft;
            return diamondHandler;
        }

        private Action<Vector3, int, int> ExcludeRightDiamonds(Action<Vector3, int, int> diamondHandler)
        {
            diamondHandler -= HandleUpperRight;
            diamondHandler -= HandleLowerRight;
            return diamondHandler;
        }

        private void HandleOriginal(Vector3 vertex, int row, int column)
        {
            output[Original(row) + column] = vertex;
        }

        private void HandleLowerRight(Vector3 vertex, int row, int column)
        {
            output[LowerDiamond(row) + column] = vertex;
        }

        private void HandleLowerLeft(Vector3 vertex, int row, int column)
        {
            output[LowerDiamond(row) + column - 1].y += vertex.y;
        }

        private void HandleUpperRight(Vector3 vertex, int row, int column)
        {
            output[UpperDiamond(row) + column].y += vertex.y;
        }

        private void HandleUpperLeft(Vector3 vertex, int row, int column)
        {
            output[UpperDiamond(row) + column - 1].y += vertex.y;
            output[UpperDiamond(row) + column - 1].y /= 4;
            output[UpperDiamond(row) + column - 1] += xzShift;
        }

        private int Original(int row) => row * (2 * verticesInRow - 1);
        private int UpperDiamond(int row) => Original(row) - verticesInRow + 1;
        private int LowerDiamond(int row) => Original(row) + verticesInRow;
    }
}