using UnityEngine;

namespace ProceduralToolkit
{
    public class GeneratorBootFactory : IGeneratorBootFactory
    {
        public void CreateGeneratorBoot(BaseShapeGeneratorSettings baseShape)
        {
            var boot = new GameObject().AddComponent<GeneratorBoot>();
            boot.BaseShape = baseShape;
            boot.name = "GeneratorBoot";
        }
    }
}
