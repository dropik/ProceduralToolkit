using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class LandscapeContext
    {
        public Vector3[] Vertices { get; set; }
        public int[] Indices { get; set; }
        public int Length { get; set; } = 2;
        public int Iterations { get; set; }
        public float SideLength { get; set; }
        public Vector3 GridSize { get; set; }
    }
}
