using ProceduralToolkit.Models.FSM;
using System.Collections.Generic;

namespace ProceduralToolkit.Services.Generators
{
    public class DiamondsMask
    {
        public int Length { get; set; }
        public IEnumerable<bool> Mask
        {
            get
            {
                var context = CreateContext();
                return GetMaskWithContext(context);
            }
        }

        private DiamondsMaskContext CreateContext() => new DiamondsMaskContext()
        {
            NewVertices = Length - 1,
            TotalVertices = 2 * Length - 1
        };

        private IEnumerable<bool> GetMaskWithContext(DiamondsMaskContext context)
        {
            while (context.Row < Length)
            {
                foreach (var item in GetColumnMask(context))
                {
                    yield return item;
                }
            }
        }

        private IEnumerable<bool> GetColumnMask(DiamondsMaskContext context)
        {
            foreach (var item in GetOriginalRowItems(context))
            {
                yield return item;
            }

            foreach (var item in TryGetDiamondRowItems(context))
            {
                yield return item;
            }

            GoToNextRow(context);
        }

        private IEnumerable<bool> GetOriginalRowItems(DiamondsMaskContext context)
        {
            while (context.Column < Length)
            {
                yield return false;
                context.Column++;
            }
        }

        private IEnumerable<bool> TryGetDiamondRowItems(DiamondsMaskContext context)
        {
            if (!IsLastRow(context))
            {
                while (context.Column < context.TotalVertices)
                {
                    yield return true;
                    context.Column++;
                }
            }
        }

        private void GoToNextRow(DiamondsMaskContext context)
        {
            context.Column = 0;
            context.Row++;
        }

        private bool IsLastRow(DiamondsMaskContext context) => context.Row == Length - 1;
    }
}
