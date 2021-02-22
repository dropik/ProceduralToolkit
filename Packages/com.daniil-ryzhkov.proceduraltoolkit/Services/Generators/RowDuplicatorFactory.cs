using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Services.Generators
{
    public static class RowDuplicatorFactory
    {
        public static FSMBasedGenerator Create()
        {
            return new FSMBasedGenerator((vertices, columns) =>
            {
                var context = new FSMContext(columns)
                {
                    RowDuplicatorContext = new RowDuplicatorContext(columns)
                };

                var settings = new FSMSettings()
                {
                    FSMContext = context,
                    ColumnsLimit = columns
                };

                var storeCopySettings = new FSMSettings()
                {
                    FSMContext = context,
                    ColumnsLimit = columns - 1,
                    ZeroColumnOnLimitReached = false
                };

                var returnOriginal = new ReturnOriginal(settings);
                var storeCopy = new StoreCopy(storeCopySettings);
                var returnCopies = new ReturnCopies(settings);

                context.State = returnOriginal;

                returnOriginal.NextState = returnOriginal;
                returnOriginal.StateWhenLimitReached = storeCopy;

                storeCopy.NextState = storeCopy;
                storeCopy.StateWhenLimitReached = returnCopies;

                returnCopies.NextState = storeCopy;
                returnCopies.StateWhenLimitReached = storeCopy;

                return context;
            });
        }
    }
}
