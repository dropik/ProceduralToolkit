using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class DiamondContext
    {
        public DiamondContext(int length, int iteration)
        {
            Iteration = iteration;
            DiamondStep = (int)((length - 1) / Mathf.Pow(2, iteration - 1));
            Step = DiamondStep / 2;
        }

        public int Iteration { get; set; }
        public int DiamondStep { get; set; }
        public int Step { get; set; }
    }
}
