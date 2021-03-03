using System.Collections.Generic;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Services.Generators
{
    public static class LeftAdderFactory
    {
        public static FSMBasedGenerator Create() => new FSMBasedGenerator(columns =>
        {
            var storeOperations = 2 * (columns - 1);
            var context = new AdderContext(1) { Column = columns - 1 };
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            bool EndStoring() => context.Column >= storeOperations;
            bool EndSkips() => context.Column >= 2;

            machine.AddState("Enqueue", builder =>
            {
                builder.ConfigurePreprocessor(new StoreHeight(context))
                       .SetDefaultState("Dequeue");
            });

            machine.AddState("Dequeue", builder =>
            {
                builder.ConfigureOutput(new OutputAddedHeight(context))
                       .On(EndStoring).SetNext("OutputOriginal")
                       .SetDefaultState("Enqueue");
            });

            machine.AddState("OutputOriginal", builder =>
            {
                builder.On(EndSkips).SetNext("Enqueue")
                       .SetDefaultState("OutputOriginal");
            });

            machine.SetState("Enqueue");
            return machine;
        });
    }
}