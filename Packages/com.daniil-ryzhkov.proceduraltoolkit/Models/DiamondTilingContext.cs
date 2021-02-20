using ProceduralToolkit.Services.Generators;
using UnityEngine;

namespace ProceduralToolkit.Models
{
    public class DiamondTilingContext
    {
        public DiamondTilingContext(int columnsInRow)
        {
            this.ColumnsInRow = columnsInRow;
        }

        public int ColumnsInRow { get; }
        public IDiamondTilingState State { get; set; }
        public int Column { get; set; }
        public Vector3 First { get; set; }
        public Vector3 XZShift { get; set; }
    }
}