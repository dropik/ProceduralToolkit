using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Square
    {
        public Square()
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
                var prevLength = new IterationToLength { Iteration = Iteration }.Length;
                var newLength = new IterationToLength { Iteration = Iteration + 1 }.Length;

                var tiling = SquareTilingFactory.Create();
                tiling.InputVertices = InputVertices;
                tiling.ColumnsInRow = prevLength;

                var leftAdder = LeftAdderFactory.Create();
                leftAdder.InputVertices = tiling.OutputVertices;
                leftAdder.ColumnsInRow = newLength;

                var upAdder = UpAdderFactory.Create();
                upAdder.InputVertices = leftAdder.OutputVertices;
                upAdder.ColumnsInRow = newLength;

                var downAdder = DownAdderFactory.Create();
                downAdder.InputVertices = upAdder.OutputVertices;
                downAdder.ColumnsInRow = newLength;

                var normalizer = NormalizerFactory.Create();
                normalizer.InputVertices = downAdder.OutputVertices;
                normalizer.ColumnsInRow = newLength;

                var mask = new SquaresMask { Length = newLength };
                var displacer = new Displacer
                {
                    Settings = Settings,
                    InputVertices = normalizer.OutputVertices,
                    Iteration = Iteration,
                    Mask = mask.Mask
                };

                return displacer.OutputVertices;
            }
        }
    }
}
