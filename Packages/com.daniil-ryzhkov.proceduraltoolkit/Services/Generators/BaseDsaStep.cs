using ProceduralToolkit.Models;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public abstract class BaseDsaStep : IDsaStep
    {
        private readonly IDisplacer displacer;

        protected LandscapeContext Context { get; private set; }

        public BaseDsaStep(LandscapeContext context, IDisplacer displacer)
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
                Context.Vertices[GetIndex(row, column)] = GetStepVertex(row, column, context);
            }
        }

        protected abstract IEnumerable<(int row, int column)> GetRowsAndColumnsForStep(DsaStepContext context);

        private Vector3 GetStepVertex(int row, int column, DsaStepContext context)
        {
            var vertex = GetVertexOnGrid(row, column);
            vertex.y = GetNeighboursHeightAverage(row, column, context.HalfStep);
            vertex.y += displacer.GetDisplacement(context.Iteration);
            return vertex;
        }

        protected int GetIndex(int row, int column)
            => row * Context.Length + column;

        private Vector3 GetVertexOnGrid(int row, int column)
            => Vector3.Scale(new Vector3(1, 0, 1), Context.Vertices[0] + GetShift(row, column));

        private Vector3 GetShift(int row, int column)
            => Vector3.Scale(new Vector3(column, 0, row), Context.GridSize);

        protected abstract float GetNeighboursHeightAverage(int row, int column, int step);
    }
}
