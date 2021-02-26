using System.Collections.Generic;
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

                bool RowEnd() => context.Column >= columns;

                var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

                machine.AddState("StoreFirst", builder =>
                {
                    builder.ConfigurePreprocessor(new StoreFirst(context.DiamondTilingContext))
                           .SetDefaultState("OutputOriginal1");
                });

                machine.AddState("OutputOriginal1", builder =>
                {
                    builder.SetDefaultState("OutputOriginal1")
                           .On(RowEnd).SetNext("OutputSkip1");
                });

                machine.AddState("OutputSkip1", builder => 
                {
                    builder.ConfigureOutput(new OutputSkip())
                           .SetDefaultState("CalculateShift")
                           .On(RowEnd).SetNext("OutputOriginal2");
                });

                machine.AddState("CalculateShift", builder =>
                {
                    builder.ConfigureOutput(new OutputDiamond(context.DiamondTilingContext))
                           .ConfigurePreprocessor(new CalculateXZShift(context.DiamondTilingContext))
                           .SetDefaultState("OutputDiamond1")
                           .On(RowEnd).SetNext("OutputOriginal2");
                });

                machine.AddState("OutputDiamond1", builder =>
                {
                    builder.ConfigureOutput(new OutputDiamond(context.DiamondTilingContext))
                           .SetDefaultState("OutputDiamond1")
                           .On(RowEnd).SetNext("OutputOriginal2");
                });

                machine.AddState("OutputOriginal2", builder => 
                {
                    builder.SetDefaultState("OutputOriginal2")
                           .On(RowEnd).SetNext("OutputSkip2");
                });

                machine.AddState("OutputSkip2", builder =>
                {
                    builder.ConfigureOutput(new OutputSkip())
                           .SetDefaultState("OutputDiamond2")
                           .On(RowEnd).SetNext("OutputOriginal2");
                });

                machine.AddState("OutputDiamond2", builder =>
                {
                    builder.ConfigureOutput(new OutputDiamond(context.DiamondTilingContext))
                           .SetDefaultState("OutputDiamond2")
                           .On(RowEnd).SetNext("OutputOriginal2");
                });

                machine.SetState("StoreFirst");
                return machine;
            });
        }
    }
}
