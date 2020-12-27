using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit
{
    [CreateAssetMenu(menuName = "Procedural Toolkit/Plane Settings")]
    public class PlaneGeneratorSettings : BaseShapeGeneratorSettings
    {
        public float length = 1f;
        public float width = 1f;

        protected override IGenerator CreateGenerator()
        {
            return new PlaneGenerator(length, width);
        }
    }
}
