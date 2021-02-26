using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public static class InvertorFactory
    {
        public static FSMBasedGenerator Create() => new Invertor(columns =>
        {
            var context = new InvertorContext(columns);
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            bool OriginalRowEnd() => context.Column >= columns;
            bool DiamondRowEnd() => context.Column >= columns - 1;

            machine.AddState("OutputOriginal1", builder =>
            {
                builder.SetDefaultState("OutputOriginal1")
                       .On(OriginalRowEnd).SetNext("EnqueueVertex");
            });

            machine.AddState("EnqueueVertex", builder =>
            {
                builder.ConfigureOutput(new OutputSkip())
                       .ConfigurePreprocessor(new EnqueueVertex(context))
                       .SetDefaultState("EnqueueVertex")
                       .On(DiamondRowEnd).SetNext("OutputOriginal2");
            });

            machine.AddState("OutputOriginal2", builder =>
            {
                builder.SetDefaultState("OutputOriginal2")
                       .On(OriginalRowEnd).SetNext("OutputDequeued");
            });

            machine.AddState("OutputDequeued", builder =>
            {
                builder.ConfigureOutput(new OutputDequeued(context))
                       .ConfigurePreprocessor(new EnqueueVertex(context))
                       .SetDefaultState("OutputDequeued")
                       .On(DiamondRowEnd).SetNext("OutputOriginal2");
            });

            machine.SetState("OutputOriginal1");
            return machine;
        });
    }
}
