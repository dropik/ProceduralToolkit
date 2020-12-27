using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    [ExecuteInEditMode]
    public class GeneratorBoot : MonoBehaviour
    {
        private LandscapeGenerator landscapeGenerator;
        private IGenerator generator;

        public void Awake()
        {
            var newObj = new GameObject();
            newObj.name = "LandscapeGenerator";
            newObj.transform.parent = transform;
            landscapeGenerator = newObj.AddComponent<LandscapeGenerator>();
            landscapeGenerator.DefaultMaterialProvider = new DefaultMaterialProvider();
        }

        public BaseShapeGeneratorSettings BaseShape
        {
            set
            {
                generator = value;
                landscapeGenerator.MeshBuilder = new MeshBuilder(generator);
            }
        }

        public void Start()
        {
            landscapeGenerator.Generate();
        }
    }
}