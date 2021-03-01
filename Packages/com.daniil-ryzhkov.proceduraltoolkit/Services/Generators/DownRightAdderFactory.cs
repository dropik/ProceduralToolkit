using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public static class DownRightAdderFactory
    {
        public static FSMBasedGenerator Create() => new FSMBasedGenerator(columns =>
        {
            var context = new AdderContext(columns);
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            bool Always() => true;
            bool OriginalRowEnd() => context.Column >= columns;
            bool DiamondRowEnd() => context.Column >= columns - 1;

            machine.AddState("OutputOriginal", builder =>
            {
                builder.SetDefaultState("OutputOriginal")
                       .On(OriginalRowEnd).SetNext("SkipStoringFirst");
            });

            machine.AddState("SkipStoringFirst", builder =>
            {
                builder.On(Always).SetNext("StoreHeight").DoNotZeroColumn();
            });

            machine.AddState("StoreHeight", builder =>
            {
                builder.ConfigurePreprocessor(new StoreHeight(context))
                       .SetDefaultState("StoreHeight")
                       .On(OriginalRowEnd).SetNext("OutputMean");
            });

            machine.AddState("OutputMean", builder =>
            {
                builder.ConfigureOutput(new OutputMeanHeight(context))
                       .SetDefaultState("OutputMean")
                       .On(DiamondRowEnd).SetNext("SkipStoringFirst");
            });

            machine.SetState("OutputOriginal");
            return machine;
        });
    }
}
