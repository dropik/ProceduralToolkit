using System.Collections.Generic;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class ReturnOriginal : IState
    {
        private readonly IEnumerator<Vector3> inputVerticesEnumerator;
        private readonly DiamondContext context;

        public ReturnOriginal(IEnumerator<Vector3> inputVerticesEnumerator, DiamondContext context)
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
            if (context.Column >= context.Length)
            {
                context.Column = 0;
                context.State = NextState;
            }
        }
    }
}