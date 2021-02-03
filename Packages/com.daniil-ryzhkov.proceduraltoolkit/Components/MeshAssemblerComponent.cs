using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    public class MeshAssemblerComponent : MonoBehaviour
    {
        [Service]
        private readonly IMeshAssembler meshAssembler;

        private void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        public void Start()
        {
            meshAssembler.Assemble();
        }
    }
}