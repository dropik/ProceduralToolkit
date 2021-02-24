using ProceduralToolkit.Models.FSM;
using ProceduralToolkit.Services.Generators.FSM;

namespace ProceduralToolkit.Services.Generators
{
    public static class DiamondTilingFactory
    {
        public static FSMBasedGenerator Create()
        {
            return new FSMBasedGenerator(columns =>
            {
                var context = new FSMContext()
                {
                    DiamondTilingContext = new DiamondTilingContext()
                };
                var factory = new StateFactory(context);

                var storeFirst = factory.ConfigureBuilder(builder =>
                {
                    builder.ConfigurePreprocessor(new StoreFirst(context.DiamondTilingContext));
                }).CreateState();

                var outputOriginal1 = factory.CreateState();
                var outputOriginal2 = factory.CreateState();

                var outputDiamond1 = factory.ConfigureBuilder(builder =>
                {
                    builder.ConfigureOutput(new OutputDiamond(context.DiamondTilingContext));
                }).CreateState();
                var outputDiamond2 = factory.ConfigureBuilder(builder =>
                {
                    builder.ConfigureOutput(new OutputDiamond(context.DiamondTilingContext));
                }).CreateState();

                var outputSkip1 = factory.ConfigureBuilder(builder =>
                {
                    builder.ConfigureOutput(new OutputSkip());
                }).CreateState();
                var outputSkip2 = factory.ConfigureBuilder(builder =>
                {
                    builder.ConfigureOutput(new OutputSkip());
                }).CreateState();

                var calculateShift = factory.ConfigureBuilder(builder =>
                {
                    builder.ConfigurePreprocessor(new CalculateXZShift(context.DiamondTilingContext))
                           .ConfigureOutput(new OutputDiamond(context.DiamondTilingContext));
                }).CreateState();

                bool RowEnd() => context.Column >= columns;
                storeFirst.SetDefaultNext(outputOriginal1);
                outputOriginal1
                    .SetDefaultNext(outputOriginal1)
                    .On(RowEnd).SetNext(outputSkip1);
                outputSkip1
                    .SetDefaultNext(calculateShift)
                    .On(RowEnd).SetNext(outputOriginal2);
                calculateShift
                    .SetDefaultNext(outputDiamond1)
                    .On(RowEnd).SetNext(outputOriginal2);
                outputDiamond1
                    .SetDefaultNext(outputDiamond1)
                    .On(RowEnd).SetNext(outputOriginal2);
                outputOriginal2
                    .SetDefaultNext(outputOriginal2)
                    .On(RowEnd).SetNext(outputSkip2);
                outputSkip2
                    .SetDefaultNext(outputDiamond2)
                    .On(RowEnd).SetNext(outputOriginal2);
                outputDiamond2
                    .SetDefaultNext(outputDiamond2)
                    .On(RowEnd).SetNext(outputOriginal2);

                context.State = storeFirst;
                return context;
            });
        }
    }
}
