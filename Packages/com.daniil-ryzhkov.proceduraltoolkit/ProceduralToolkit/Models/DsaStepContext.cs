using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class DsaStepContext
    {
        public DsaStepContext(int length, int iteration)
        {
            Iteration = iteration;
            Step = (int)((length - 1) / Mathf.Pow(2, iteration - 1));
            HalfStep = Step / 2;
        }

        public int Iteration { get; }
        public int Step { get; }
        public int HalfStep { get; }
    }
}
