using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public abstract class BaseState : IStateBehaviour
    {
        protected FSMSettings Settings { get; private set; }

        public BaseState(FSMSettings settings)
        {
            Settings = settings;
        }

        public IStateBehaviour NextState { get; set; }
        public IStateBehaviour StateWhenLimitReached { get; set; }

        public IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            PreprocessVertex(vertex);
            SwitchState();
            return GetResultVertices(vertex);
        }

        protected virtual void PreprocessVertex(Vector3 vertex) { }

        private void SwitchState()
        {
            var context = Settings.FSMContext;
            context.Column++;
            if (context.Column >= Settings.ColumnsLimit)
            {
                if (Settings.ZeroColumnOnLimitReached)
                {
                    context.Column = 0;
                }
                context.State = StateWhenLimitReached;
            }
            else
            {
                context.State = NextState;
            }
        }

        protected abstract IEnumerable<Vector3> GetResultVertices(Vector3 vertex);
    }
}
