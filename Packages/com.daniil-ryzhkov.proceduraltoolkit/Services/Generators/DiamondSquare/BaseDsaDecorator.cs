namespace ProceduralToolkit.Services.Generators.DiamondSquare
{
    public abstract class BaseDsaDecorator : IDsa
    {
        private readonly IDsa wrappee;

        public BaseDsaDecorator(IDsa wrappee)
        {
            this.wrappee = wrappee;
        }

        public virtual void Execute()
        {
            wrappee.Execute();
        }
    }
}