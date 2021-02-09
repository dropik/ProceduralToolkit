using UnityEngine;
using System.Collections.Generic;
using System;

namespace ProceduralToolkit.Services
{
    public class MeshBuilder : IMeshBuilder
    {
        private readonly Func<IEnumerable<Vector3>> verticesProvider;
        private readonly Func<IEnumerable<int>> trianglesProvider;

        private IEnumerator<Vector3> verticesEnumerator;
        private IEnumerator<int> trianglesEnumerator;
        private List<Vector3> verticesList;
        private List<int> trianglesList;

        public MeshBuilder(Func<IEnumerable<Vector3>> verticesProvider, Func<IEnumerable<int>> trianglesProvider)
        {
            this.verticesProvider = verticesProvider;
            this.trianglesProvider = trianglesProvider;
        }

        public Mesh Build()
        {
            var mesh = new Mesh();
            BuildToMesh(mesh);
            return mesh;
        }

        private void BuildToMesh(Mesh mesh)
        {
            SaveEnumeratorsToMesh(mesh);
            mesh.RecalculateNormals();
        }

        private void SaveEnumeratorsToMesh(Mesh mesh)
        {
            ProcessEnumeratorsToLists();
            SaveListsToMesh(mesh);
            ClearLists();
        }

        private void ProcessEnumeratorsToLists()
        {
            UpdateEnumerators();
            InitLists();
            FillLists();
        }

        private void SaveListsToMesh(Mesh mesh)
        {
            mesh.vertices = verticesList.ToArray();
            mesh.triangles = trianglesList.ToArray();
        }

        private void ClearLists()
        {
            verticesList.Clear();
            trianglesList.Clear();
        }

        private void UpdateEnumerators()
        {
            verticesEnumerator = verticesProvider.Invoke().GetEnumerator();
            trianglesEnumerator = trianglesProvider.Invoke().GetEnumerator();
        }

        private void InitLists()
        {
            verticesList = new List<Vector3>();
            trianglesList = new List<int>();
        }

        private void FillLists()
        {
            while (trianglesEnumerator.MoveNext())
            {
                HandleOneIndex();
            }
        }

        private void HandleOneIndex()
        {
            TryAddVertex();
            trianglesList.Add(trianglesEnumerator.Current);
        }

        private void TryAddVertex()
        {
            if (verticesEnumerator.MoveNext())
            {
                verticesList.Add(verticesEnumerator.Current);
            }
        }
    }
}
