using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class DiamondContext
    {
        public DiamondContext(Vector3[] vertices, int length, int iteration)
        {
            Iteration = iteration;
            DiamondStep = (int)((length - 1) / Mathf.Pow(2, iteration - 1));
            Step = DiamondStep / 2;

            var xzShift = (vertices[DiamondStep * length + DiamondStep] - vertices[0]) / 2f;
            xzShift.y = 0;
            XzShift = xzShift;
        }

        public int Iteration { get; set; }
        public int DiamondStep { get; set; }
        public int Step { get; set; }
        public Vector3 XzShift { get; set; }
    }
}
