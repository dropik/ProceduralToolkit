using ProceduralToolkit.Models.FSMContexts;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public abstract class BaseDiamondTilingState : IDiamondTilingState
    {
        protected FSMContext Context { get; private set; }

        public BaseDiamondTilingState(FSMContext context)
        {
            Context = context;
        }

        public IDiamondTilingState StateWhenRowContinues { get; set; }
        public IDiamondTilingState StateWhenEndedRow { get; set; }

        public Vector3? MoveNext(Vector3 vertex)
        {
            PreprocessVertex(vertex);
            SwitchState();
            return GetResultVertex(vertex);
        }

        protected virtual void PreprocessVertex(Vector3 vertex) { }

        private void SwitchState()
        {
            Context.Column++;
            if (Context.Column >= Context.ColumnsInRow)
            {
                Context.Column = 0;
                Context.State = StateWhenEndedRow;
            }
            else
            {
                Context.State = StateWhenRowContinues;
            }
        }

        protected abstract Vector3? GetResultVertex(Vector3 vertex);
    }
}
