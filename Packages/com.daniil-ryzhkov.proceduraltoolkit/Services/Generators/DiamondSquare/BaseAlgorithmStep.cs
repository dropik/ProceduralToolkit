using ProceduralToolkit.Models;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public abstract class BaseAlgorithmStep : IAlgorithmStep
    {
        private readonly IDisplacer displacer;

        protected LandscapeContext Context { get; private set; }

        public BaseAlgorithmStep(LandscapeContext context, IDisplacer displacer)
        {
            Context = context;
            this.displacer = displacer;
        }

        public void Execute(int iteration)
        {
            var context = new DsaStepContext(Context.Length, iteration);
            HandleStep(context);
        }

        private void HandleStep(DsaStepContext context)
        {
            foreach (var (row, column) in GetRowsAndColumnsForStep(context))
            {
                Context.Heights[row, column] = GetNeighboursHeightAverage(row, column, context.HalfStep) +
                                               displacer.GetNormalizedDisplacement(context.Iteration);
            }
        }

        protected abstract IEnumerable<(int row, int column)> GetRowsAndColumnsForStep(DsaStepContext context);

        protected abstract float GetNeighboursHeightAverage(int row, int column, int halfStep);
    }
}
