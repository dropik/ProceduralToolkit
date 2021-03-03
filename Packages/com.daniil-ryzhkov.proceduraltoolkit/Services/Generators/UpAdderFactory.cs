using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public static class UpAdderFactory
    {
        public static FSMBasedGenerator Create() => new FSMBasedGenerator(columns =>
        {
            var queueSize = (columns + 1) / 2;
            var context = new AdderContext(queueSize);
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            bool RowEnd() => context.Column >= columns;

            machine.AddState("StoreHeight1", builder =>
            {
                builder.ConfigurePreprocessor(new StoreHeight(context))
                       .On(RowEnd).SetNext("AddHeight")
                       .SetDefaultState("OutputOriginal");
            });

            machine.AddState("OutputOriginal", builder =>
            {
                builder.SetDefaultState("StoreHeight1");
            });

            machine.AddState("AddHeight", builder =>
            {
                builder.ConfigureOutput(new OutputAddedHeight(context))
                       .SetDefaultState("StoreHeight2");
            });

            machine.AddState("StoreHeight2", builder =>
            {
                builder.ConfigurePreprocessor(new StoreHeight(context))
                       .SetDefaultState("AddHeight");
            });

            machine.SetState("StoreHeight1");
            return machine;
        });
    }
}
