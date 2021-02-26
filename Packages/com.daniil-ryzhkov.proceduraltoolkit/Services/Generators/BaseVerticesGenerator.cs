using System.Collections.Generic;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators
{
    public abstract class BaseVerticesGenerator
    {
        public virtual IEnumerable<Vector3> InputVertices
        {
            get => inputVertices ?? (new Vector3[0]);
            set => inputVertices = value;
        }
        private IEnumerable<Vector3> inputVertices;

        public abstract IEnumerable<Vector3> OutputVertices { get; }
    }
}
