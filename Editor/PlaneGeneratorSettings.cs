using UnityEngine;

namespace ProceduralToolkit
{
    [CreateAssetMenu(menuName = "Procedural Toolkit/Plane Settings")]
    public class PlaneGeneratorSettings : BaseShapeGeneratorSettings
    {
        public float length = 1f;
        public float width = 1f;

        protected override IGenerator CreateGenerator()
        {
            return new PlaneGenerator(Vector3.zero, length, width);
        }
    }
}
