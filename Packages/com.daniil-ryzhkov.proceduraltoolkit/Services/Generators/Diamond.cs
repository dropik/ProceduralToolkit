using System;
using System.Collections.Generic;
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

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                var index = 0;
                foreach (var vertex in inputVertices)
                {
                    GenerateVerticesFor(vertex, index);
                    index++;
                }
                return output;
            }
        }

        private void GenerateVerticesFor(Vector3 vertex, int index)
        {
            var row = index / verticesInRow;
            var column = index % verticesInRow;

            HandleOriginal(vertex, row, column);

            Action<Vector3, int, int> diamondHandler = HandleUpperLeft;
            diamondHandler += HandleUpperRight;
            diamondHandler += HandleLowerLeft;
            diamondHandler += HandleLowerRight;

            if (row == 0)
            {
                diamondHandler -= HandleUpperLeft;
                diamondHandler -= HandleUpperRight;
            }

            if (row == verticesInRow - 1)
            {
                diamondHandler -= HandleLowerLeft;
                diamondHandler -= HandleLowerRight;
            }

            if (column == 0)
            {
                diamondHandler -= HandleUpperLeft;
                diamondHandler -= HandleLowerLeft;
            }

            if (column == verticesInRow - 1)
            {
                diamondHandler -= HandleUpperRight;
                diamondHandler -= HandleLowerRight;
            }

            diamondHandler(vertex, row, column);
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
        private int UpperDiamond(int row) => Original(row) + - verticesInRow + 1;
        private int LowerDiamond(int row) => Original(row) + verticesInRow;
    }
}