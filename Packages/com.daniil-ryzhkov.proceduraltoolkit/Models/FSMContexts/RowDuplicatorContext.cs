using UnityEngine;

namespace ProceduralToolkit.Models.FSMContexts
{
    public sealed class RowDuplicatorContext
    {
        public RowDuplicatorContext(int columnsInRow)
        {
            VerticesCopies = new Vector3[columnsInRow];
        }

        public Vector3[] VerticesCopies { get; }
    }
}
