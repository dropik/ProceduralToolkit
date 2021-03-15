using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public abstract class BaseDsaStep : IDsaStep
    {
        protected Vector3[] Vertices { get; private set; }
        protected int Length { get; private set; }
        protected Vector3 GridSize { get; private set; }
        protected IDisplacer Displacer { get; private set; }

        public BaseDsaStep(LandscapeContext context, IDisplacer displacer)
        {
            Vertices = context.Vertices;
            Length = context.Length;
            GridSize = context.GridSize;
            Displacer = displacer;
        }

        public void Execute(int iteration)
        {
            var context = new DsaStepContext(Length, iteration);
            HandleStep(context);
        }

        protected abstract void HandleStep(DsaStepContext context);
    }
}
