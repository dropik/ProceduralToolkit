using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public abstract class BaseStateDecorator : IState
    {
        private readonly IState wrappee;
        protected FSMSettings Settings { get; private set; }

        public BaseStateDecorator(IState wrappee, FSMSettings settings)
        {
            this.wrappee = wrappee;
            Settings = settings;
        }

        public virtual IEnumerable<Vector3> MoveNext(Vector3 vertex)
        {
            return wrappee.MoveNext(vertex);
        }
    }
}
