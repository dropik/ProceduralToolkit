using ProceduralToolkit.Services;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [ExecuteInEditMode]
    public class GeneratorStarterComponent : MonoBehaviour
    {
        [Service]
        private readonly IGeneratorStarter starter;

        private void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        public void Start()
        {
            SendMessage("TryUpdateSettings", SendMessageOptions.DontRequireReceiver);
            starter?.Start();
        }
    }
}