using UnityEngine;

namespace ProceduralToolkit
{
    [ExecuteInEditMode]
    public class LandscapeGeneratorRoot : MonoBehaviour
    {
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
        }

        public void Start()
        {
            LandscapeGenerator.Generate();
        }
    }
}