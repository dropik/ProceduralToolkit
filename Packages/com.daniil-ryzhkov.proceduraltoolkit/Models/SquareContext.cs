using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class SquareContext
    {
        public SquareContext(int length, int iteration)
        {
            Iteration = iteration;
            SquareStep = (int)((length - 1) / Mathf.Pow(2, iteration - 1));
            Step = SquareStep / 2;
        }

        public int Iteration { get; }
        public int SquareStep { get; }
        public int Step { get; }
    }
}
