using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class DiamondContext
    {
        public DiamondContext(Vector3[] vertices, int iteration)
        {
            Vertices = vertices;
            Iteration = iteration;
            Length = (int)Mathf.Sqrt(vertices.Length);
            DiamondStep = (int)((Length - 1) / Mathf.Pow(2, iteration - 1));
            Step = DiamondStep / 2;

            var xzShift = (vertices[DiamondStep * Length + DiamondStep] - vertices[0]) / 2f;
            xzShift.y = 0;
            XzShift = xzShift;
        }

        public Vector3[] Vertices { get; set; }
        public int Iteration { get; set; }
        public int Length { get; set; }
        public int DiamondStep { get; set; }
        public int Step { get; set; }
        public Vector3 XzShift { get; set; }
    }
}
