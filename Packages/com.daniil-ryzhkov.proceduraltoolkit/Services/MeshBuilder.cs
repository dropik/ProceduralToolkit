using UnityEngine;
using System.Collections.Generic;
using System;

namespace ProceduralToolkit.Services
{
    public class MeshBuilder : IMeshBuilder
    {
        private readonly Func<IEnumerable<Vector3>> verticesProvider;
        private readonly Func<IEnumerable<int>> trianglesProvider;

        private Mesh resultingMesh;

        public MeshBuilder(Func<IEnumerable<Vector3>> verticesProvider, Func<IEnumerable<int>> trianglesProvider)
        {
            this.verticesProvider = verticesProvider;
            this.trianglesProvider = trianglesProvider;
        }

        public Mesh Build()
        {
            resultingMesh = new Mesh();
            var verticesEnumerator = verticesProvider.Invoke().GetEnumerator();
            var trianglesEnumerator = trianglesProvider.Invoke().GetEnumerator();
            var verticesList = new List<Vector3>();
            var trianglesList = new List<int>();
            while (trianglesEnumerator.MoveNext())
            {
                if (verticesEnumerator.MoveNext())
                {
                    verticesList.Add(verticesEnumerator.Current);
                }
                trianglesList.Add(trianglesEnumerator.Current);
            }
            resultingMesh.vertices = verticesList.ToArray();
            resultingMesh.triangles = trianglesList.ToArray();
            resultingMesh.RecalculateNormals();
            return resultingMesh;
        }
    }
}
