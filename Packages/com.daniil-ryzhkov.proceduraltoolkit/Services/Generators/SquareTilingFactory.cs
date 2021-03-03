using System.Collections.Generic;
using ProceduralToolkit.Models.FSM;

namespace ProceduralToolkit.Services.Generators.FSM
{
    public static class SquareTilingFactory
    {
        public static FSMBasedGenerator Create() => new FSMBasedGenerator(columns =>
        {
            var context = new TilingContext();
            var machine = new Machine(new Dictionary<string, IState>(), () => new StateBuilder(context));

            var withDiamondsVertices = 2 * (columns - 1);
            bool DiamondsEnd() => context.Column >= withDiamondsVertices;
            bool Always() => true;

            machine.AddState("StoreFirst", builder =>
            {
                builder.ConfigurePreprocessor(new StoreFirst(context))
                       .SetDefaultState("CalculateShift");
            });

            machine.AddState("CalculateShift", builder =>
            {
                builder.ConfigureOutput(new OutputNewSquare(context))
                       .ConfigurePreprocessor(new CalculateXZShift(context))
                       .On(DiamondsEnd).SetNext("OutputTwoSquares").DoNotZeroColumn()
                       .SetDefaultState("OutputNewSquare");
            });

            machine.AddState("OutputNewSquare", builder =>
            {
                builder.ConfigureOutput(new OutputNewSquare(context))
                       .On(DiamondsEnd).SetNext("OutputTwoSquares").DoNotZeroColumn()
                       .SetDefaultState("OutputNewSquare");
            });

            machine.AddState("OutputTwoSquares", builder =>
            {
                builder.ConfigureOutput(new OutputTwoNewSquares(context))
                       .On(Always).SetNext("OutputOriginal");
            });

            machine.AddState("OutputOriginal", builder =>
            {
                builder.SetDefaultState("OutputNewSquare");
            });

            machine.SetState("StoreFirst");
            return machine;
        });
    }
}