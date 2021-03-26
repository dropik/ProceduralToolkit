namespace ProceduralToolkit.Services
{
    public class GeneratorScheduler
    {
        private readonly IGeneratorStarter starter;

        public GeneratorScheduler(IGeneratorStarter starter)
        {
            this.starter = starter;
        }

        private bool isDirty;

        public void Update()
        {
            if (isDirty)
            {
                starter?.Start();
                isDirty = false;
            }
        }

        public void MarkDirty()
        {
            isDirty = true;
        }
    }
}