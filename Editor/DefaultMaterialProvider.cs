using UnityEngine;

namespace ProceduralToolkit
{
    public class DefaultMaterialProvider : IMaterialProvider
    {
        public Material GetMaterial()
        {
            return new Material(Shader.Find("Standard"));
        }
    }
}
