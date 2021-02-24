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
                var factory = new StateFactory(context);

                var outputOriginal = factory.CreateState();
                var storeCopy = factory.ConfigureBuilder(builder =>
                {
                    builder.ConfigurePreprocessor(new StoreCopy(context));
                }).CreateState();
                var outputCopies = factory.ConfigureBuilder(builder =>
                {
                    builder.ConfigurePreprocessor(new StoreCopy(context))
                           .ConfigureOutput(new OutputCopies(context.RowDuplicatorContext));
                }).CreateState();

                bool RowEnd() => context.Column >= columns;
                bool AlmostRowEnd() => context.Column >= columns - 1;
                outputOriginal
                    .SetDefaultNext(outputOriginal)
                    .On(RowEnd).SetNext(storeCopy);
                storeCopy
                    .SetDefaultNext(storeCopy)
                    .On(AlmostRowEnd).SetNext(outputCopies).DoNotZeroColumn();
                outputCopies
                    .SetDefaultNext(outputCopies)
                    .On(RowEnd).SetNext(storeCopy);

                context.State = outputOriginal;
                return context;
            });
        }
    }
}
