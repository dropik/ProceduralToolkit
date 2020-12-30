using ProceduralToolkit.Api;
using UnityEngine;

namespace ProceduralToolkit
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [ExecuteInEditMode]
    public class LandscapeGeneratorRoot : MonoBehaviour
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

        private T GetLazy<T>()
            where T : Component
        {
            return GetComponent<T>() ?? gameObject.AddComponent<T>();
        }

        private LandscapeGenerator LandscapeGenerator =>
            GetLazy<LandscapeGenerator>();

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
            LandscapeGenerator.Reset();
            PlaneSettings.Reset();
        }

        public void OnValidate()
        {
            var generator = PlaneSettings;
            LandscapeGenerator.MeshBuilder = new MeshBuilder(generator);
            LandscapeGenerator.DefaultMaterialProvider = new DefaultMaterialProvider();
            LandscapeGenerator.MeshContainer = new MeshContainer(GetComponent<MeshFilter>());
            LandscapeGenerator.MaterialContainer = new MaterialContainer(GetComponent<MeshRenderer>());
        }

        public void Start()
        {
            LandscapeGenerator.Generate();
        }
    }
}