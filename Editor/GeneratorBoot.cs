using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    [ExecuteInEditMode]
    public class GeneratorBoot : MonoBehaviour
    {
        public BaseShapeGeneratorSettings baseShape;

        private LandscapeGenerator landscapeGenerator;
        private IGenerator generator;

        public void Awake()
        {
            landscapeGenerator = GetComponentInChildren<LandscapeGenerator>();
            if (landscapeGenerator == null)
            {
                var newObj = new GameObject
                {
                    name = "LandscapeGenerator"
                };
                newObj.transform.parent = transform;
                landscapeGenerator = newObj.AddComponent<LandscapeGenerator>();
            }
            else
            {
                BuildGenerator();
            }

            landscapeGenerator.DefaultMaterialProvider = new DefaultMaterialProvider();
        }

        public void BuildGenerator()
        {
            generator = baseShape;
            landscapeGenerator.MeshBuilder = new MeshBuilder(generator);
        }

        public void Start()
        {
            landscapeGenerator.Generate();
        }
    }
}