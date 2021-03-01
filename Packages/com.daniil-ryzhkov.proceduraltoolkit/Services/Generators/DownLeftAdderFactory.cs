using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public static class DownLeftAdderFactory
    {
        public static FSMBasedGenerator Create() => new FSMBasedGenerator(columns =>
        {
            var context = new AdderContext(columns);
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            bool RowEnd() => context.Column >= columns;
            bool AlmostRowEnd() => context.Column >= columns - 1;

            machine.AddState("OutputOriginal", builder =>
            {
                builder.SetDefaultState("OutputOriginal")
                       .On(RowEnd).SetNext("StoreHeight");
            });

            machine.AddState("StoreHeight", builder =>
            {
                builder.ConfigurePreprocessor(new StoreHeight(context))
                       .SetDefaultState("StoreHeight")
                       .On(AlmostRowEnd).SetNext("SkipStoringLast").DoNotZeroColumn();
            });

            machine.AddState("SkipStoringLast", builder =>
            {
                builder.On(RowEnd).SetNext("AddHeight");
            });

            machine.AddState("AddHeight", builder =>
            {
                builder.ConfigureOutput(new OutputAddedHeight(context))
                       .SetDefaultState("AddHeight")
                       .On(AlmostRowEnd).SetNext("StoreHeight");
            });

            machine.SetState("OutputOriginal");
            return machine;
        });
    }
}
