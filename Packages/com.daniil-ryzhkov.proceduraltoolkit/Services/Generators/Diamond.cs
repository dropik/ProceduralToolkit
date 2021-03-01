using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Diamond
    {
        public Diamond()
        {
            InputVertices = new Vector3[0];
            Settings = new DSASettings();
        }

        public IEnumerable<Vector3> InputVertices { get; set; }
        public int Iteration { get; set; }
        public DSASettings Settings { get; set; }

        public IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var iterationToLength = new IterationToLength()
                {
                    Iteration = Iteration
                };
                var length = iterationToLength.Length;

                var duplicator = RowDuplicatorFactory.Create();
                duplicator.InputVertices = InputVertices;
                duplicator.ColumnsInRow = length;

                var tiling = DiamondTilingFactory.Create();
                tiling.InputVertices = duplicator.OutputVertices;
                tiling.ColumnsInRow = length;

                var upLeftAdder = UpLeftAdderFactory.Create();
                upLeftAdder.InputVertices = tiling.OutputVertices;
                upLeftAdder.ColumnsInRow = length;

                var upRightAdder = UpRightAdderFactory.Create();
                upRightAdder.InputVertices = upLeftAdder.OutputVertices;
                upRightAdder.ColumnsInRow = length;

                var invertor = InvertorFactory.Create();
                invertor.InputVertices = upRightAdder.OutputVertices;
                invertor.ColumnsInRow = length;

                var downLeftAdder = DownLeftAdderFactory.Create();
                downLeftAdder.InputVertices = invertor.OutputVertices;
                downLeftAdder.ColumnsInRow = length;

                var downRightAdder = DownRightAdderFactory.Create();
                downRightAdder.InputVertices = downLeftAdder.OutputVertices;
                downRightAdder.ColumnsInRow = length;

                var backInvertor = BackInvertorFactory.Create();
                backInvertor.InputVertices = downRightAdder.OutputVertices;
                backInvertor.ColumnsInRow = length;

                var mask = new DiamondsMask()
                {
                    Length = length
                };

                var displacer = new Displacer()
                {
                    InputVertices = backInvertor.OutputVertices,
                    Settings = Settings,
                    Mask = mask.Mask,
                    Iteration = Iteration
                };

                return displacer.OutputVertices;
            }
        }
    }
}