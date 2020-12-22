using System;
using System.Collections.Generic;

namespace Easynvest.Api.Models
{
    public class ExtratoInvestimentoModel
    {
        public double ValorTotal { get; set; }
        public List<InvestimentoModel> Investimentos { get; set; }
    }

    public class InvestimentoModel
    {
        public string Nome { get; set; }
        public double ValorInvestido { get; set; }
        public double ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public double Ir { get; set; }
        public double ValorResgate { get; set; }
    }
}
