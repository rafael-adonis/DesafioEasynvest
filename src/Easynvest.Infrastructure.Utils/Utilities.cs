using System;

namespace Easynvest.Infrastructure.Utils
{
    public static class Utilities
    {
        public static int DiferencaEmMeses(DateTime dataInicial, DateTime dataFinal) =>
            (((dataInicial.Year - dataFinal.Year) * 12) + dataInicial.Month - dataFinal.Month) * -1;
    }
}
