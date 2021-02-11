using System.Collections.Generic;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Displacer
    {
        private readonly IEnumerable<Vector3> inputVertices;
        private readonly DSASettings settings;
        private readonly int iteration;

        private IEnumerator<Vector3> verticesEnumerator;
        private IEnumerator<bool> maskEnumerator;

        public Displacer(IEnumerable<Vector3> vertices, DSASettings settings, int iteration)
        {
            inputVertices = vertices;
            this.settings = settings;
            this.iteration = iteration;
            Mask = new bool[0];
        }

        public IEnumerable<bool> Mask { get; set; }

        public IEnumerable<Vector3> Vertices
        {
            get
            {
                Random.InitState(settings.Seed);
                UpdateEnumerators();
                while (verticesEnumerator.MoveNext())
                {
                    yield return DisplacedVertex;
                }
            }
        }

        private void UpdateEnumerators()
        {
            verticesEnumerator = inputVertices.GetEnumerator();
            maskEnumerator = Mask.GetEnumerator();
        }

        private Vector3 DisplacedVertex
        {
            get
            {
                var newVertex = GetCurrentVertexCopy();
                if (CanApplyDisplacement)
                {
                    ApplyDisplacementTo(ref newVertex);
                }
                return newVertex;
            }
        }

        private Vector3 GetCurrentVertexCopy()
        {
            var vertex = verticesEnumerator.Current;
            return new Vector3(vertex.x, vertex.y, vertex.z);
        }

        private bool CanApplyDisplacement => !maskEnumerator.MoveNext() || maskEnumerator.Current;

        private void ApplyDisplacementTo(ref Vector3 vertex)
        {
            var magnitude = Mathf.Pow(2, -1 * iteration * settings.Hardness);
            vertex.y += Random.Range(-magnitude, magnitude);
        }
    }
}