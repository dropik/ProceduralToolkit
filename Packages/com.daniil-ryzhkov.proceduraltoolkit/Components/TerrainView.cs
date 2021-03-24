using ProceduralToolkit.Models;
using ProceduralToolkit.Services.DI;
using UnityEngine;

namespace ProceduralToolkit.Components
{
    [ExecuteInEditMode]
    public class TerrainView : MonoBehaviour, IView
    {
        [Service]
        private readonly TerrainData terrainData;
        [Service]
        private readonly LandscapeContext context;

        public void Awake()
        {
            hideFlags = HideFlags.HideInInspector;
        }

        public void Update()
        {
            if (IsDirty)
            {
                terrainData.SetHeights(0, 0, context.Heights);
                IsDirty = false;
            }
        }

        public void MarkDirty()
        {
            IsDirty = true;
        }

        public bool IsDirty { get; private set; }
    }
}
