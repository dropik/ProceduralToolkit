using ProceduralToolkit.Models.FSM;
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

                var storeFirstBase = new ReturnOriginal(settings);
                var returnDiamond1Base = new ReturnOriginal(settings);
                var returnDiamond2Base = new ReturnOriginal(settings);
                var skipVertex1Base = new ReturnOriginal(settings);
                var skipVertex2Base = new ReturnOriginal(settings);
                var calculateShiftBase = new ReturnOriginal(settings);

                var storeFirst = new StoreFirst(storeFirstBase, settings);
                var returnOriginal1 = new ReturnOriginal(settings);
                var returnOriginal2 = new ReturnOriginal(settings);
                var returnDiamond1 = new ReturnDiamond(returnDiamond1Base, settings);
                var returnDiamond2 = new ReturnDiamond(returnDiamond2Base, settings);
                var skipVertex1 = new SkipVertex(skipVertex1Base, settings);
                var skipVertex2 = new SkipVertex(skipVertex2Base, settings);
                var calculateShift = new CalculateXZShift(new ReturnDiamond(calculateShiftBase, settings), settings);

                context.StateBehaviour = storeFirst;

                storeFirstBase.NextState = returnOriginal1;

                returnOriginal1.NextState = returnOriginal1;
                returnOriginal1.StateWhenLimitReached = skipVertex1;

                skipVertex1Base.NextState = calculateShift;
                skipVertex1Base.StateWhenLimitReached = returnOriginal2;

                calculateShiftBase.NextState = returnDiamond1;
                calculateShiftBase.StateWhenLimitReached = returnOriginal2;

                returnDiamond1Base.NextState = returnDiamond1;
                returnDiamond1Base.StateWhenLimitReached = returnOriginal2;

                returnOriginal2.NextState = returnOriginal2;
                returnOriginal2.StateWhenLimitReached = skipVertex2;

                skipVertex2Base.NextState = returnDiamond2;
                skipVertex2Base.StateWhenLimitReached = returnOriginal2;

                returnDiamond2Base.NextState = returnDiamond2;
                returnDiamond2Base.StateWhenLimitReached = returnOriginal2;

                return context;
            });
        }
    }
}
