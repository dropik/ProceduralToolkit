using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class DiamondSquareIteration
    {
        public DiamondSquareIteration()
        {
            Settings = new DSASettings();
            InputVertices = new Vector3[0];
        }

        public DSASettings Settings { get; set; }
        public IEnumerable<Vector3> InputVertices { get; set; }
        public int Iteration { get; set; }

        public IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var diamond = new Diamond { Settings = Settings, InputVertices = InputVertices, Iteration = Iteration };
                var square = new Square { Settings = Settings, InputVertices = diamond.OutputVertices, Iteration = Iteration };
                return square.OutputVertices;
            }
        }
    }
}
