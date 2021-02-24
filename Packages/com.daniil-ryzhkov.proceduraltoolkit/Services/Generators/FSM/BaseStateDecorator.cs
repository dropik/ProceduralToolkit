using ProceduralToolkit.Models.FSMContexts;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public abstract class BaseStateDecorator : IStateBehaviour
    {
        private readonly IStateBehaviour wrappee;
        protected FSMSettings Settings { get; private set; }

        public BaseStateDecorator(IStateBehaviour wrappee, FSMSettings settings)
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
