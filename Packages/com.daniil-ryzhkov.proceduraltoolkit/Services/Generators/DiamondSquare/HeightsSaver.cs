using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class HeightsSaver : BaseDsaDecorator
    {
        private readonly TerrainData terrainData;
        private readonly LandscapeContext context;

        public HeightsSaver(IDsa wrappee, TerrainData terrainData, LandscapeContext context)
            : base(wrappee)
        {
            this.terrainData = terrainData;
            this.context = context;
        }

        public override void Execute()
        {
            base.Execute();
            if (context.Heights != null)
            {
                terrainData.SetHeights(0, 0, context.Heights);
            }
        }
    }
}