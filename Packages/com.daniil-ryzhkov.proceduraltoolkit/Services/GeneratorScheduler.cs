using System;
using ProceduralToolkit.Services.Generators.DiamondSquare;

namespace ProceduralToolkit.Services
{
    public class GeneratorScheduler
    {
        private readonly IDsa dsa;

        public event Action Generated;

        public GeneratorScheduler(IDsa dsa)
        {
            this.dsa = dsa;
        }

        private bool isDirty;

        public void Update()
        {
            if (isDirty)
            {
                dsa?.Execute();
                Generated?.Invoke();
                isDirty = false;
            }
        }

        public void MarkDirty()
        {
            isDirty = true;
        }
    }
}