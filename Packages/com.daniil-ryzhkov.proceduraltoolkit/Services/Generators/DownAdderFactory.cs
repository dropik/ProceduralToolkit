using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public static class DownAdderFactory
    {
        public static FSMBasedGenerator Create() => new BackInvertor(columns =>
        {
            var context = new InvertorContext(columns + 1);
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            bool RowEnd() => context.Column >= columns;

            machine.AddState("OutputSkip", builder =>
            {
                builder.ConfigureOutput(new OutputSkip())
                       .ConfigurePreprocessor(new EnqueueVertex(context))
                       .SetDefaultState("OutputSkip")
                       .On(RowEnd).SetNext("OutputDequeued");
            });

            machine.AddState("OutputDequeued", builder =>
            {
                builder.ConfigureOutput(new OutputDequeued(context))
                       .ConfigurePreprocessor(new EnqueueVertex(context))
                       .SetDefaultState("OutputDequeuedWithHeight");
            });

            machine.AddState("OutputDequeuedWithHeight", builder =>
            {
                builder.ConfigureOutput(new OutputDequeuedWithHeight(context))
                       .ConfigurePreprocessor(new EnqueueVertex(context))
                       .SetDefaultState("OutputDequeued");
            });

            machine.SetState("OutputSkip");
            return machine;
        });
    }
}
