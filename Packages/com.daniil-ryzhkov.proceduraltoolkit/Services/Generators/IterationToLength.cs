using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class IterationToLength
    {
        public int Iteration { get; set; }
        public int Length => (int)Mathf.Pow(2, Iteration) + 1;
    }
}
