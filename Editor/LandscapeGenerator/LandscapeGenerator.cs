using UnityEngine;

namespace ProceduralToolkit.Landscape
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [ExecuteInEditMode]
    public class LandscapeGenerator : MonoBehaviour
    {
        private MeshAssembler meshAssembler;

        private PlaneSettings PlaneSettings => GetLazy<PlaneSettings>();

        private T GetLazy<T>()
            where T : Component
        {
            return GetComponent<T>() ?? gameObject.AddComponent<T>();
        }

        public void Awake()
        {
            if (name == "New Game Object")
            {
                name = "LandscapeGenerator";
            }
        }

        public void Reset()
        {
            PlaneSettings.Reset();
            GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        }

        public void OnValidate()
        {
            var generator = PlaneSettings;

            meshAssembler = new MeshAssembler(new MeshBuilder(generator));
            meshAssembler.Generated += (mesh) => GetComponent<MeshFilter>().sharedMesh = mesh;
        }

        public void Start()
        {
            meshAssembler.Assemble();
        }
    }
}