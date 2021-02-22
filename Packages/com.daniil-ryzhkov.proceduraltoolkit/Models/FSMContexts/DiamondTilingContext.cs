using UnityEngine;

namespace ProceduralToolkit.Models.FSMContexts
{
    public class DiamondTilingContext : FSMContext
    {
        public DiamondTilingContext(int columnsInRow) : base(columnsInRow) { }

        public Vector3 First { get; set; }
        public Vector3 XZShift { get; set; }
    }
}