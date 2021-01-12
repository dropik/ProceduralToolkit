using UnityEditor;
using UnityEngine;
using ProceduralToolkit.Components.GeneratorSettings;
using ProceduralToolkit.Services;
using ProceduralToolkit.Services.Generators;

namespace ProceduralToolkit.Components
{
    [RequireComponent(typeof(PlaneSettings))]
    [ExecuteInEditMode]
    public class LandscapeGenerator : MonoBehaviour
    {
        private MeshAssembler meshAssembler;

        private PlaneSettings PlaneSettings => GetComponent<PlaneSettings>();
        private IGeneratorView GeneratorView => GetComponentInChildren<IGeneratorView>();

        public void Reset()
        {
            ResetName();
            ResetSettings();
            RemoveOldHierarchy();
            InitView();
        }

        private void ResetName()
        {
            name = "LandscapeGenerator";
        }

        private void ResetSettings()
        {
            PlaneSettings.Reset();
        }

        private void RemoveOldHierarchy()
        {
            foreach (var child in transform)
            {
                DestroyImmediate(((Transform)child).gameObject);
            }
        }

        private void InitView()
        {
            var viewObj = new GameObject()
            {
                name = "view"
            };
            viewObj.transform.parent = transform;
            viewObj.AddComponent<GeneratorViewRoot>();
        }

        public void OnValidate()
        {
            var generator = ConstructGenerator();
            InitMeshAssembler(generator);
            SetupUpdateCallbacks();
        }

        private IGenerator ConstructGenerator()
        {
            return PlaneSettings;
        }

        private void InitMeshAssembler(IGenerator generator)
        {
            meshAssembler = new MeshAssembler(new MeshBuilder(generator));
            meshAssembler.Generated += (mesh) => GeneratorView.NewMesh = mesh;
        }

        private void SetupUpdateCallbacks()
        {
            PlaneSettings.GeneratorUpdated += meshAssembler.Assemble;
        }

        public void Start()
        {
            meshAssembler.Assemble();
        }

        public void RegisterUndo()
        {
            Undo.RegisterCreatedObjectUndo(gameObject, "New Landscape Generator");
        }
    }
}