using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    [ExecuteInEditMode]
    public class LandscapeGeneratorRoot : MonoBehaviour
    {
        public BaseShapeGeneratorSettings baseShape;

        private IGenerator generator;

        private LandscapeGenerator LandscapeGenerator =>
            GetComponent<LandscapeGenerator>();

        public void Awake()
        {
            name = "LandscapeGenerator";
        }

        public void Reset()
        {
            DestroyImmediate(LandscapeGenerator);
            gameObject.AddComponent<LandscapeGenerator>();
            baseShape = ScriptableObject.CreateInstance<PlaneGeneratorSettings>();
        }

        public void OnValidate()
        {
            generator = baseShape;
            LandscapeGenerator.MeshBuilder = new MeshBuilder(generator);
            LandscapeGenerator.DefaultMaterialProvider = new DefaultMaterialProvider();
        }

        public void Start()
        {
            LandscapeGenerator.Generate();
        }
    }
}