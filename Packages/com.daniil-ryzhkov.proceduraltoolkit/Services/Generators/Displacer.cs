using System.Collections.Generic;
using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public class Displacer
    {
        private IEnumerator<Vector3> verticesEnumerator;
        private IEnumerator<bool> maskEnumerator;

        public Displacer()
        {
            InputVertices = new Vector3[0];
            Settings = new DSASettings();
            Mask = new bool[0];
        }

        public IEnumerable<Vector3> InputVertices { get; set; }
        public DSASettings Settings { get; set; }
        public int Iteration { get; set; }
        public IEnumerable<bool> Mask { get; set; }

        public IEnumerable<Vector3> OutputVertices
        {
            get
            {
                Random.InitState(Settings.Seed);
                UpdateEnumerators();
                while (verticesEnumerator.MoveNext())
                {
                    yield return DisplacedVertex;
                }
            }
        }

        private void UpdateEnumerators()
        {
            verticesEnumerator = InputVertices.GetEnumerator();
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
            var magnitude = Mathf.Pow(2, -1 * Iteration * Settings.Hardness) * Settings.Magnitude;
            vertex.y += Random.Range(-magnitude, magnitude);
        }
    }
}