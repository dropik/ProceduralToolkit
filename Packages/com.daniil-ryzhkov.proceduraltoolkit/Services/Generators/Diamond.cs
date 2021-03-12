using System;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly Vector3[] input;
        private readonly int verticesInRow;
        private readonly Vector3 xzShift;
        private readonly Vector3[] output;

        public Diamond(Vector3[] input, int iteration)
        {
            this.input = input;

            verticesInRow = (int)Mathf.Pow(2, iteration) + 1;

            output = new Vector3[2 * verticesInRow * (verticesInRow - 1) + 1];

            xzShift = (input[verticesInRow + 1] - input[0]) / 2f;
            xzShift.y = 0;
        }

        public Vector3[] Output
        {
            get
            {
                for (int i = 1; i < 2 * verticesInRow - 1; i += 2)
                {
                    for (int j = 0; j < verticesInRow - 1; j++)
                    {
                        HandleOriginal(input[i / 2 * verticesInRow + j], OriginalIndex(i / 2, j));
                        HandleOriginal(input[i / 2 * verticesInRow + j + 1], OriginalIndex(i / 2, j + 1));
                        HandleOriginal(input[(i + 1) / 2 * verticesInRow + j], OriginalIndex((i + 1) / 2, j));
                        HandleOriginal(input[(i + 1) / 2 * verticesInRow + j + 1], OriginalIndex((i + 1) / 2, j + 1));

                        output[(i / 2) * (2 * verticesInRow - 1) + verticesInRow + j] = output[OriginalIndex(i / 2, j)] + xzShift;
                        output[(i / 2) * (2 * verticesInRow - 1) + verticesInRow + j].y += (output[OriginalIndex(i / 2, j + 1)].y + output[OriginalIndex((i + 1) / 2, j)].y + output[OriginalIndex((i + 1) / 2, j + 1)].y);
                        output[(i / 2) * (2 * verticesInRow - 1) + verticesInRow + j].y /= 4f;
                    }
                }
                return output;
            }
        }

        private void HandleOriginal(Vector3 vertex, int index)
        {
            output[index] = vertex;
        }

        private int Original(int row) => row * (2 * verticesInRow - 1);
        private int OriginalIndex(int row, int column) => Original(row) + column;
    }
}