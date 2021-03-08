using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class DiamondSquare
    {
        public DiamondSquare()
        {
            Settings = new DSASettings();
            InputVertices = new Vector3[0];
        }

        public DSASettings Settings { get; set; }
        public IEnumerable<Vector3> InputVertices { get; set; }

        public IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var displacer = new Displacer() { Settings = Settings, InputVertices = InputVertices, Iteration = 0 };
                var input = displacer.OutputVertices;
                for (int i = 0; i < Settings.Iterations; i++)
                {
                    var ds = new DiamondSquareIteration { Settings = Settings, InputVertices = input, Iteration = i };
                    input = ds.OutputVertices;
                }
                return input;
            }
        }
    }
}
