using System.Collections.Generic;
using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Services.Generators
{
    public static class RowDuplicatorFactory
    {
        public static FSMBasedGenerator Create()
        {
            return new FSMBasedGenerator(columns =>
            {
                var context = new FSMContext()
                {
                    RowDuplicatorContext = new RowDuplicatorContext(columns)
                };

                bool RowEnd() => context.Column >= columns;
                bool AlmostRowEnd() => context.Column >= columns - 1;

                var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

                machine.AddState("OutputOriginal", builder =>
                {
                    builder.SetDefaultState("OutputOriginal")
                           .On(RowEnd).SetNext("StoreCopy");
                });

                machine.AddState("StoreCopy", builder =>
                {
                    builder.ConfigurePreprocessor(new StoreCopy(context))
                           .SetDefaultState("StoreCopy")
                           .On(AlmostRowEnd).SetNext("OutputCopies").DoNotZeroColumn();
                });

                machine.AddState("OutputCopies", builder =>
                {
                    builder.ConfigureOutput(new OutputCopies(context.RowDuplicatorContext))
                           .ConfigurePreprocessor(new StoreCopy(context))
                           .SetDefaultState("OutputCopes")
                           .On(RowEnd).SetNext("StoreCopy");
                });

                machine.SetState("OutputOriginal");
                return machine;
            });
        }
    }
}
