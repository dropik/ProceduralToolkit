using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly Vector3[] input;
        private readonly int iteration;
        private readonly DSASettings settings;
        private readonly int verticesInRow;
        private readonly Vector3 xzShift;

        public Diamond(Vector3[] input, int iteration, DSASettings settings)
        {
            this.input = input;
            this.iteration = iteration;
            this.settings = settings;

            verticesInRow = (int)Mathf.Pow(2, iteration) + 1;

            xzShift = (input[OriginalIndex(1, 1)] - input[0]) / 2f;
            xzShift.y = 0;
        }

        public Vector3[] Output
        {
            get
            {
                Random.InitState(settings.Seed);

                for (int i = 1; i < 2 * verticesInRow - 1; i += 2)
                {
                    for (int j = 0; j < verticesInRow - 1; j++)
                    {
                        input[(i / 2) * (2 * verticesInRow - 1) + verticesInRow + j] = input[OriginalIndex(i / 2, j)] + xzShift;
                        input[(i / 2) * (2 * verticesInRow - 1) + verticesInRow + j].y += (input[OriginalIndex(i / 2, j + 1)].y + input[OriginalIndex((i + 1) / 2, j)].y + input[OriginalIndex((i + 1) / 2, j + 1)].y);
                        input[(i / 2) * (2 * verticesInRow - 1) + verticesInRow + j].y /= 4f;
                        input[(i / 2) * (2 * verticesInRow - 1) + verticesInRow + j].y += GetElevation();
                    }
                }
                return input;
            }
        }

        private float GetElevation()
        {
            var magnitude = Mathf.Pow(2, -1 * iteration * settings.Hardness) * settings.Magnitude;
            return Random.Range(-magnitude, magnitude);
        }

        private int Original(int row) => row * (2 * verticesInRow - 1);
        private int OriginalIndex(int row, int column) => Original(row) + column;
    }
}