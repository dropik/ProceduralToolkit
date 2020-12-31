using ProceduralToolkit.Api;
using UnityEngine;

namespace ProceduralToolkit
{
    public class MaterialGeneratorView : IGeneratorView
    {
        private readonly IMaterialContainer materialContainer;
        private readonly Material defaultMaterial;

        public MaterialGeneratorView(IMaterialContainer materialContainer,
                                     Material defaultMaterial)
        {
            this.materialContainer = materialContainer;
            this.defaultMaterial = defaultMaterial;
        }

        public void OnGenerate(Mesh mesh)
        {
            if (MaterialIsNotSet)
            {
                SetDefaultMaterial();
            }
        }

        private bool MaterialIsNotSet => materialContainer.Material == null;

        private void SetDefaultMaterial()
        {
            materialContainer.Material = defaultMaterial;
        }
    }
}
