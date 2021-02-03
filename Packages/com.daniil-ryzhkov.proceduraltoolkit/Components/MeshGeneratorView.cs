using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [RequireComponent(typeof(MeshFilter))]
    [ExecuteInEditMode]
    public class MeshGeneratorView : MonoBehaviour, IGeneratorView
    {
        [Service]
        private readonly MeshFilter meshFilter;

        public Mesh NewMesh { get; set; }

        public void Update()
        {
            if (NewMesh != null)
            {
                meshFilter.sharedMesh = NewMesh;
                NewMesh = null;
            }
        }
    }
}