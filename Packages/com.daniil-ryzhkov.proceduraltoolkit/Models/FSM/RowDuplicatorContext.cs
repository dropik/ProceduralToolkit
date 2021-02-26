using UnityEngine;

namespace ProceduralToolkit.Models.FSM
{
    public class RowDuplicatorContext : FSMContext
    {
        public RowDuplicatorContext(int columnsInRow)
        {
            VerticesCopies = new Vector3[columnsInRow];
        }

        public Vector3[] VerticesCopies { get; }
    }
}
