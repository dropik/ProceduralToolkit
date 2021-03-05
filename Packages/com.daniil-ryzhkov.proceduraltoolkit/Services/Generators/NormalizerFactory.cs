using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public static class NormalizerFactory
    {
        public static FSMBasedGenerator Create() => new FSMBasedGenerator(columns =>
        {
            var context = new NormalizingContext();
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            bool AlmostRowEnd() => context.Column >= columns - 1;
            bool LastRow() => context.Row >= columns - 1;
            bool Always() => true;

            machine.AddState("OutputOriginal1", builder =>
            {
                builder.SetDefaultState("NormalizeHorizontalBound");
            });

            machine.AddState("NormalizeHorizontalBound", builder =>
            {
                builder.ConfigureOutput(new OutputNormalized(3))
                       .SetDefaultState("OutputOriginal1")
                       .On(AlmostRowEnd).SetNext("OutputOriginalAndIncrementRow").DoNotZeroColumn();
            });

            machine.AddState("OutputOriginalAndIncrementRow", builder =>
            {
                builder.ConfigurePreprocessor(new RowCounter(context))
                       .On(Always).SetNext("OutputLeft");
            });

            machine.AddState("OutputLeft", builder =>
            {
                builder.ConfigureOutput(new OutputNormalized(3))
                       .SetDefaultState("OutputOriginal2");
            });

            machine.AddState("OutputOriginal2", builder =>
            {
                builder.SetDefaultState("OutputMiddle1")
                       .On(AlmostRowEnd).SetNext("OutputRight");
            });

            machine.AddState("OutputMiddle1", builder =>
            {
                builder.ConfigureOutput(new OutputNormalized(4))
                       .SetDefaultState("OutputOriginal2");
            });

            machine.AddState("OutputRight", builder =>
            {
                builder.ConfigurePreprocessor(new RowCounter(context))
                       .ConfigureOutput(new OutputNormalized(3))
                       .On(LastRow).SetNext("OutputOriginal1")
                       .On(Always).SetNext("OutputOriginal3");
            });

            machine.AddState("OutputOriginal3", builder =>
            {
                builder.SetDefaultState("OutputMiddle2");
            });

            machine.AddState("OutputMiddle2", builder =>
            {
                builder.ConfigureOutput(new OutputNormalized(4))
                       .SetDefaultState("OutputOriginal3")
                       .On(AlmostRowEnd).SetNext("OutputOriginalAndIncrementRow");
            });

            machine.SetState("OutputOriginal1");
            return machine;
        });
    }
}
