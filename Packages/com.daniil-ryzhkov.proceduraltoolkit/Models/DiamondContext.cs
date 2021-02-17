using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class DiamondContext
    {
        public DiamondContext(int length)
        {
            this.Length = length;
            this.OriginalVertices = new Vector3[length];
        }

        public int Length { get; }
        public IState State { get; set; }
        public Vector3 Current { get; set; }
        public int Row { get; set; }
        public int Column { get; set; }
        public Vector3[] OriginalVertices { get; }
        public Vector3 XZShift { get; set; }
    }
}