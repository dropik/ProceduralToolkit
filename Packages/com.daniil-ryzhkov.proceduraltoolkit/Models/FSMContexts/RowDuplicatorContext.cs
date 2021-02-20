using ProceduralToolkit.Services.Generators.FSM;
using UnityEngine;

namespace ProceduralToolkit.Models.FSMContexts
{
    public class RowDuplicatorContext
    {
        public RowDuplicatorContext(int columnsInRow)
        {
            ColumnsInRow = columnsInRow;
            VerticesCopies = new Vector3[columnsInRow];
        }

        public IRowDuplicatorState State { get; set; }
        public int ColumnsInRow { get; }
        public Vector3[] VerticesCopies { get; }
        public Vector3 Current { get; set; }
        public int Column { get; set; }
    }
}
