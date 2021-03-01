using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public static class BackInvertorFactory
    {
        public static FSMBasedGenerator Create() => new BackInvertor(columns =>
        {
            var context = new InvertorContext(columns + 1);
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            bool OriginalRowEnd() => context.Column >= columns;
            bool DiamondRowEnd() => context.Column >= columns - 1;

            machine.AddState("OutputOriginal", builder =>
            {
                builder.SetDefaultState("OutputOriginal")
                       .On(OriginalRowEnd).SetNext("EnqueueVertex");
            });

            machine.AddState("EnqueueVertex", builder =>
            {
                builder.ConfigureOutput(new OutputSkip())
                       .ConfigurePreprocessor(new EnqueueVertex(context))
                       .SetDefaultState("EnqueueVertex")
                       .On(OriginalRowEnd).SetNext("OutputDiamond");
            });

            machine.AddState("OutputDiamond", builder =>
            {
                builder.SetDefaultState("OutputDiamond")
                       .On(DiamondRowEnd).SetNext("OutputDequeued");
            });

            machine.AddState("OutputDequeued", builder =>
            {
                builder.ConfigureOutput(new OutputDequeued(context))
                       .ConfigurePreprocessor(new EnqueueVertex(context))
                       .SetDefaultState("OutputDequeued")
                       .On(OriginalRowEnd).SetNext("OutputDiamond");
            });

            machine.SetState("OutputOriginal");
            return machine;
        });
    }
}
