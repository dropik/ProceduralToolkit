using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Services.Generators
{
    public static class DiamondTilingFactory
    {
        public static FSMBasedGenerator Create()
        {
            return new FSMBasedGenerator((vertices, columns) =>
            {
                var context = new FSMContext(columns)
                {
                    DiamondTilingContext = new DiamondTilingContext()
                };

                var settings = new FSMSettings()
                {
                    FSMContext = context,
                    ColumnsLimit = columns
                };

                var storeFirst = new StoreFirst(settings);
                var returnOriginal1 = new ReturnOriginal(settings);
                var returnOriginal2 = new ReturnOriginal(settings);
                var returnDiamond1 = new ReturnDiamond(settings);
                var returnDiamond2 = new ReturnDiamond(settings);
                var skipVertex1 = new SkipVertex(settings);
                var skipVertex2 = new SkipVertex(settings);
                var calculateShift = new CalculateXZShift(settings);

                context.State = storeFirst;

                storeFirst.NextState = returnOriginal1;

                returnOriginal1.NextState = returnOriginal1;
                returnOriginal1.StateWhenLimitReached = skipVertex1;

                skipVertex1.NextState = calculateShift;
                skipVertex1.StateWhenLimitReached = returnOriginal2;

                calculateShift.NextState = returnDiamond1;
                calculateShift.StateWhenLimitReached = returnOriginal2;

                returnDiamond1.NextState = returnDiamond1;
                returnDiamond1.StateWhenLimitReached = returnOriginal2;

                returnOriginal2.NextState = returnOriginal2;
                returnOriginal2.StateWhenLimitReached = skipVertex2;

                skipVertex2.NextState = returnDiamond2;
                skipVertex2.StateWhenLimitReached = returnOriginal2;

                returnDiamond2.NextState = returnDiamond2;
                returnDiamond2.StateWhenLimitReached = returnOriginal2;

                return context;
            });
        }
    }
}
