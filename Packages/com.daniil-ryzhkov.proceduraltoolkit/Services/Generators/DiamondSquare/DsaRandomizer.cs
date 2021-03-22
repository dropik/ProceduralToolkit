using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class DsaRandomizer : BaseDsaDecorator
    {
        private readonly DsaSettings settings;

        public DsaRandomizer(IDsa wrappee, DsaSettings settings) : base(wrappee)
        {
            this.settings = settings;
        }

        public override void Execute()
        {
            Random.InitState(settings.Seed);
            base.Execute();
        }
    }
}