using ProceduralToolkit.Models.FSMContexts;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Services.Generators
{
    public partial class DiamondTiling
    {
        public static DiamondTiling Create()
        {
            return new DiamondTiling((vertices, columns) =>
            {
                var context = new FSMContext(columns);

                var storeFirst = new StoreFirst(context);
                var returnOriginal1 = new ReturnOriginal(context);
                var returnOriginal2 = new ReturnOriginal(context);
                var returnDiamond1 = new ReturnDiamond(context);
                var returnDiamond2 = new ReturnDiamond(context);
                var skipVertex1 = new SkipVertex(context);
                var skipVertex2 = new SkipVertex(context);
                var calculateShift = new CalculateXZShift(context);

                context.State = storeFirst;

                storeFirst.StateWhenRowContinues = returnOriginal1;

                returnOriginal1.StateWhenRowContinues = returnOriginal1;
                returnOriginal1.StateWhenEndedRow = skipVertex1;

                skipVertex1.StateWhenRowContinues = calculateShift;
                skipVertex1.StateWhenEndedRow = returnOriginal2;

                calculateShift.StateWhenRowContinues = returnDiamond1;
                calculateShift.StateWhenEndedRow = returnOriginal2;

                returnDiamond1.StateWhenRowContinues = returnDiamond1;
                returnDiamond1.StateWhenEndedRow = returnOriginal2;

                returnOriginal2.StateWhenRowContinues = returnOriginal2;
                returnOriginal2.StateWhenEndedRow = skipVertex2;

                skipVertex2.StateWhenRowContinues = returnDiamond2;
                skipVertex2.StateWhenEndedRow = returnOriginal2;

                returnDiamond2.StateWhenRowContinues = returnDiamond2;
                returnDiamond2.StateWhenEndedRow = returnOriginal2;

                return context;
            });
        }
    }
}
