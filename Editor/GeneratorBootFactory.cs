using UnityEngine;
using ProceduralToolkit.Api;
using UnityEditor;

namespace ProceduralToolkit
{
    public class GeneratorBootFactory : IGeneratorBootFactory
    {
        public void CreateGeneratorBoot(BaseShapeGeneratorSettings baseShape)
        {
            var boot = new GameObject().AddComponent<GeneratorBoot>();
            boot.baseShape = baseShape;
            boot.name = "GeneratorBoot";
            boot.BuildGenerator();
            Undo.RegisterCreatedObjectUndo(boot.gameObject, "New Landscape Generator");
        }
    }
}
