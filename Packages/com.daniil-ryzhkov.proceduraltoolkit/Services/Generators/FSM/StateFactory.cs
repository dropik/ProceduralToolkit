using ProceduralToolkit.Models.FSM;
using System;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public class StateFactory : IStateFactory
    {
        private readonly FSMContext context;

        private Action<IStateBuilder> build;

        public StateFactory(FSMContext context)
        {
            this.context = context;
        }

        public IStateFactory ConfigureBuilder(Action<IStateBuilder> build)
        {
            this.build = build;
            return this;
        }

        public IState CreateState()
        {
            var builder = new StateBuilder(context);
            builder.ConfigureOutput(new OutputOriginal());
            build?.Invoke(builder);
            build = default;
            return builder.Build();
        }
    }
}
