using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        private readonly Vector3[] input;
        private readonly int iteration;
        private readonly DSASettings settings;
        private readonly int length;
        private readonly Vector3 xzShift;

        public Diamond(Vector3[] input, int iteration, DSASettings settings)
        {
            this.input = input;
            this.iteration = iteration;
            this.settings = settings;

            length = (int)Mathf.Sqrt(input.Length);

            xzShift = (input[2 * (length + 1)] - input[0]) / 2f;
            xzShift.y = 0;
        }

        public Vector3[] Output
        {
            get
            {
                Random.InitState(settings.Seed);

                for (int i = 1; i < length; i += 2)
                {
                    for (int j = 1; j < length; j += 2)
                    {
                        input[i * length + j] = input[(i - 1) * length + j - 1] + xzShift;
                        input[i * length + j].y += (input[(i - 1) * length + j + 1].y + input[(i + 1) * length + j - 1].y + input[(i + 1) * length + j + 1].y);
                        input[i * length + j].y /= 4f;
                        input[i * length + j].y += GetElevation();
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
    }
}