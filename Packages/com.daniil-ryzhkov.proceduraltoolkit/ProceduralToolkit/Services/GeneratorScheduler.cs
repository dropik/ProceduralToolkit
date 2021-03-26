using ProceduralToolkit.Services.Generators.DiamondSquare;

namespace ProceduralToolkit.Services
{
    public class GeneratorScheduler
    {
        private readonly IDsa dsa;

        private bool isDirty = true;

        public GeneratorScheduler(IDsa dsa)
        {
            this.dsa = dsa;
        }

        public void Update()
        {
            if (isDirty)
            {
                dsa?.Execute();
                isDirty = false;
            }
        }

        public void MarkDirty()
        {
            isDirty = true;
        }
    }
}