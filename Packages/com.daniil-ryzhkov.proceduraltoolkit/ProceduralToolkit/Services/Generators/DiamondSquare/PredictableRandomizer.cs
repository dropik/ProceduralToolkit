using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class PredictableRandomizer : BaseDsaDecorator
    {
        private readonly DsaSettings settings;

        public PredictableRandomizer(IDsa wrappee, DsaSettings settings) : base(wrappee)
        {
            this.settings = settings;
        }

        public override void Execute()
        {
            Random.InitState(settings.seed);
            base.Execute();
        }
    }
}