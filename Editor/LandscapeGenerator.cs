using ProceduralToolkit.Api;
using UnityEngine;

namespace ProceduralToolkit
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [ExecuteInEditMode]
    public class LandscapeGenerator : MonoBehaviour
    {
        internal class MeshContainer : IMeshContainer
        {
            private readonly MeshFilter meshFilter;

            public MeshContainer(MeshFilter meshFilter)
            {
                this.meshFilter = meshFilter;
            }

            public Mesh Mesh
            {
                get => meshFilter.sharedMesh;
                set => meshFilter.sharedMesh = value;
            }
        }

        internal class MaterialContainer : IMaterialContainer
        {
            private readonly MeshRenderer meshRenderer;

            public MaterialContainer(MeshRenderer meshRenderer)
            {
                this.meshRenderer = meshRenderer;
            }

            public Material Material
            {
                get => meshRenderer.sharedMaterial;
                set => meshRenderer.sharedMaterial = value;
            }
        }

        private MeshAssembler meshAssembler;

        private T GetLazy<T>()
            where T : Component
        {
            return GetComponent<T>() ?? gameObject.AddComponent<T>();
        }

        private PlaneSettings PlaneSettings =>
            GetLazy<PlaneSettings>();

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
        }

        public void OnValidate()
        {
            var generator = PlaneSettings;
            var meshContainer = new MeshContainer(GetComponent<MeshFilter>());
            var materialContainer = new MaterialContainer(GetComponent<MeshRenderer>());
            var defaultMaterial = new Material(Shader.Find("Standard"));
            var meshGeneratorView = new MeshGeneratorView(meshContainer);
            var materialGeneratorView = new MaterialGeneratorView(materialContainer, defaultMaterial);

            meshAssembler = new MeshAssembler(new MeshBuilder(generator));
            meshAssembler.Generated += meshGeneratorView.OnGenerate;
            meshAssembler.Generated += materialGeneratorView.OnGenerate;
        }

        public void Start()
        {
            meshAssembler.Assemble();
        }
    }
}