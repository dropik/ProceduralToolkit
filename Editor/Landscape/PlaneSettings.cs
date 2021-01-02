using UnityEngine;
using ProceduralToolkit.Api;

namespace ProceduralToolkit.Landscape
{
    public class PlaneSettings : BaseSettings
    {
        [Min(0.0f)]
        public float length;
        [Min(0.0f)]
        public float width;

        public void Reset()
        {
            length = 1f;
            width = 1f;
        }

        protected override IGenerator CreateGenerator()
        {
            return new PlaneGenerator(length, width);
        }
    }
}
