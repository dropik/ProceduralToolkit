using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [ExecuteInEditMode]
    public class GeneratorStarterComponent : MonoBehaviour
    {
        [Service]
        private readonly IGeneratorStarter meshAssembler;

        private void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        public void Start()
        {
            SendMessage("TryUpdateSettings", SendMessageOptions.DontRequireReceiver);
            meshAssembler?.Start();
        }
    }
}