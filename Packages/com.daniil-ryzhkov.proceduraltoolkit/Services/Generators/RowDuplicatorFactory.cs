using ProceduralToolkit.Models.FSM;
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

                var storeCopyBase = new ReturnOriginal(storeCopySettings);
                var returnCopiesBase = new ReturnOriginal(settings);

                var returnOriginal = new ReturnOriginal(settings);
                var storeCopy = new StoreCopy(storeCopyBase, storeCopySettings);
                var returnCopies = new ReturnCopies(new StoreCopy(returnCopiesBase, settings), settings);

                context.StateBehaviour = returnOriginal;

                returnOriginal.NextState = returnOriginal;
                returnOriginal.StateWhenLimitReached = storeCopy;

                storeCopyBase.NextState = storeCopy;
                storeCopyBase.StateWhenLimitReached = returnCopies;

                returnCopiesBase.NextState = storeCopy;
                returnCopiesBase.StateWhenLimitReached = storeCopy;

                return context;
            });
        }
    }
}
