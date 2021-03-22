using ProceduralToolkit.Models;
using UnityEngine;

namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public class DsaRandomizer : IDsa
    {
        private readonly IDsa wrappee;
        private readonly DsaSettings settings;

        public DsaRandomizer(IDsa wrappee, DsaSettings settings)
        {
            this.wrappee = wrappee;
            this.settings = settings;
        }

        public void Execute()
        {
            Random.InitState(settings.Seed);
            wrappee.Execute();
        }
    }
}