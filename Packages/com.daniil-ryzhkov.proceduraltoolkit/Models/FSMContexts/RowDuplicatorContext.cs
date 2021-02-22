using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.Models.FSMContexts
{
    public sealed class RowDuplicatorContext
    {
        public RowDuplicatorContext(int columnsInRow)
        {
            VerticesCopies = new Vector3[columnsInRow];
        }

        public IRowDuplicatorState State { get; set; }
        public Vector3[] VerticesCopies { get; }
        public Vector3 Current { get; set; }
    }
}
