using ProceduralToolkit.Services;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    [ExecuteInEditMode]
    public class GeneratorViewRoot : MonoBehaviour, IGeneratorView
    {
        private IGeneratorView generatorView;

        private IGeneratorView GeneratorView
        {
            get
            {
                if (generatorView == null)
                {
                    generatorView = new GeneratorView(GetComponent<MeshFilter>());
                }
                return generatorView;
            }
        }

        public Mesh NewMesh
        {
            get => GeneratorView.NewMesh;
            set => GeneratorView.NewMesh = value;
        }

        public void Reset()
        {
            SetDefaultMaterial();
        }

        private void SetDefaultMaterial()
        {
            GetComponent<MeshRenderer>().sharedMaterial = new Material(Shader.Find("Standard"));
        }

        public void Update()
        {
            GeneratorView.Update();
        }
    }
}
