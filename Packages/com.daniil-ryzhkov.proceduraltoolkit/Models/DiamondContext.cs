using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class DiamondContext
    {
        public int Length { get; set; }
        public IState State { get; set; }
        public Vector3 Current { get; set; }
        public int Column { get; set; }
    }
}