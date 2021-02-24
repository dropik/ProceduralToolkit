using ProceduralToolkit.Models.FSM;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class FSMBasedGenerator : ColumnsBasedGenerator
    {
        private readonly Func<IEnumerable<Vector3>, int, FSMContext> contextProvider;

        public FSMBasedGenerator(Func<IEnumerable<Vector3>, int, FSMContext> contextProvider)
        {
            this.contextProvider = contextProvider;
        }

        public override IEnumerable<Vector3> OutputVertices
        {
            get
            {
                var context = contextProvider.Invoke(InputVertices, ColumnsInRow);
                return OutputVerticesForContext(context);
            }
        }

        private IEnumerable<Vector3> OutputVerticesForContext(FSMContext context)
        {
            foreach (var vertex in InputVertices)
            {
                foreach (var newVertex in GenerateFromVertex(vertex, context))
                {
                    yield return newVertex;
                }
            }
        }

        private IEnumerable<Vector3> GenerateFromVertex(Vector3 vertex, FSMContext context)
        {
            var nextVertices = GetNextVertices(vertex, context);
            return TryEnumerateNextVertices(nextVertices, vertex);
        }

        private IEnumerable<Vector3> GetNextVertices(Vector3 vertex, FSMContext context)
        {
            return context.State?.MoveNext(vertex);
        }

        private IEnumerable<Vector3> TryEnumerateNextVertices(IEnumerable<Vector3> nextVertices, Vector3 vertex)
        {
            if (nextVertices == null)
            {
                yield return vertex;
            }
            else
            {
                foreach (var next in nextVertices)
                {
                    yield return next;
                }
            }
        }
    }
}