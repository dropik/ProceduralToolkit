using ProceduralToolkit.Models;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class HeightsInitializer : BaseDsaDecorator
    {
        private readonly LandscapeContext context;
        private readonly DsaSettings settings;

        public HeightsInitializer(IDsa wrappee, LandscapeContext context, DsaSettings settings)
            : base(wrappee)
        {
            this.context = context;
            this.settings = settings;
        }

        public override void Execute()
        {
            context.Heights = CreateHeightsBuffer();
            base.Execute();
        }

        private float[,] CreateHeightsBuffer()
        {
            var heights = new float[context.Length, context.Length];
            if (context.Length > 0)
            {
                SetCornerHeights(heights);
            }
            return heights;
        }

        private void SetCornerHeights(float[,] heights)
        {
            heights[0, 0] = settings.bias;
            heights[0, context.Length - 1] = settings.bias;
            heights[context.Length - 1, 0] = settings.bias;
            heights[context.Length - 1, context.Length - 1] = settings.bias;
        }
    }
}
