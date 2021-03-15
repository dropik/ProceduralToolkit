using ProceduralToolkit.Models;
using System.Collections.Generic;
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

        private void HandleStep(DsaStepContext context)
        {
            foreach (var (row, column) in GetRowsAndColumnsForStep(context))
            {
                Vertices[GetIndex(row, column)] = GetStepVertex(row, column, context);
            }
        }

        protected abstract IEnumerable<(int row, int column)> GetRowsAndColumnsForStep(DsaStepContext context);

        private Vector3 GetStepVertex(int row, int column, DsaStepContext context)
        {
            var vertex = GetVertexOnGrid(row, column);
            vertex.y = GetNeighboursHeightAverage(row, column, context.HalfStep);
            vertex.y += Displacer.GetDisplacement(context.Iteration);
            return vertex;
        }

        protected int GetIndex(int row, int column)
            => row * Length + column;

        private Vector3 GetVertexOnGrid(int row, int column)
            => Vector3.Scale(new Vector3(1, 0, 1), Vertices[0] + GetShift(row, column));

        private Vector3 GetShift(int row, int column)
            => Vector3.Scale(new Vector3(column, 0, row), GridSize);

        protected abstract float GetNeighboursHeightAverage(int row, int column, int step);
    }
}
