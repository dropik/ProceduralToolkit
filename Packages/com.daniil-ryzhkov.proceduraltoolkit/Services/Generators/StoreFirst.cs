using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class StoreFirst : IState
    {
        private readonly IEnumerator<Vector3> inputVerticesEnumerator;
        private readonly DiamondContext context;

        public StoreFirst(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
        {
            this.inputVerticesEnumerator = inputVerticesEnumerator;
            this.context = context;
        }

        public IState NextState { get; set; }

        public void MoveNext()
        {
            inputVerticesEnumerator.MoveNext();
            context.Current = inputVerticesEnumerator.Current;
            context.Column++;
            context.State = NextState;
        }
    }
}
