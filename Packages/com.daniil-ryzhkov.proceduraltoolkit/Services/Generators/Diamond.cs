using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly Vector3[] input;
        private readonly int iteration;
        private readonly IDisplacer displacer;
        private readonly int length;
        private readonly Vector3 xzShift;

        public Diamond(Vector3[] input, int iteration, IDisplacer displacer)
        {
            this.input = input;
            this.iteration = iteration;
            this.displacer = displacer;

            length = (int)Mathf.Sqrt(input.Length);

            xzShift = (input[GetIndex(2, 2)] - input[0]) / 2f;
            xzShift.y = 0;
        }

        public Vector3[] Output
        {
            get
            {
                for (int i = 1; i < length; i += 2)
                {
                    for (int j = 1; j < length; j += 2)
                    {
                        input[GetIndex(i, j)] = input[GetIndex(i - 1, j - 1)] + xzShift;

                        input[GetIndex(i, j)].y += input[GetIndex(i - 1, j + 1)].y;
                        input[GetIndex(i, j)].y += input[GetIndex(i + 1, j - 1)].y;
                        input[GetIndex(i, j)].y += input[GetIndex(i + 1, j + 1)].y;
                        input[GetIndex(i, j)].y /= 4f;

                        input[GetIndex(i, j)].y += Displacement;
                    }
                }
                return input;
            }
        }

        private int GetIndex(int row, int column) => row * length + column;

        private float Displacement => displacer.GetDisplacement(iteration);
    }
}