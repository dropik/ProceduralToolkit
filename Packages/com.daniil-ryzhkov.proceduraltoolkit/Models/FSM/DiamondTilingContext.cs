using UnityEngine;

namespace ProceduralToolkit.Models.FSM
{
    public class DiamondTilingContext : FSMContext
    {
        public Vector3 First { get; set; }
        public Vector3 XZShift { get; set; }
    }
}