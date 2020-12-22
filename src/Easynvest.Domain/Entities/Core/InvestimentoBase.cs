using Easynvest.Infrastructure.Utils;
using System;

namespace Easynvest.Domain.Entities.Core
{
    public abstract class InvestimentoBase : Entity
    {
        protected InvestimentoBase(double aliquotaIR, int iof, string tipo, string nome, DateTime dataCompra, DateTime dataVencimento, double valorInvestido, double valorTotal)
        {
            AliquotaIR = aliquotaIR;
            Iof = iof;
            Tipo = tipo;
            Nome = nome;
            DataCompra = dataCompra;
            DataVencimento = dataVencimento;
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
        }

        public double AliquotaIR { get; private set; }
        public int Iof { get; private set; }
        public string Tipo { get; private set; }
        public string Nome { get; private set; }
        public DateTime DataCompra { get; private set; }
        public DateTime DataVencimento { get; private set; }
        public double ValorInvestido { get; private set; }
        public double ValorTotal { get; private set; }
        public double ValorIr => ObterValorIr();
        public double ValorResgate => CalcularResgate();

        private double CalcularResgate()
        {
            if (VencimentoIgualOuMenorTresMeses())
            {
                return (ValorInvestido - (ValorInvestido * 6) / 100);
            }
            else if (PossuiTempoCustodiaSuperiorMetadeTempoDeInvestimento()) 
            {
                return (ValorInvestido - (ValorInvestido * 15) / 100);
            }
            else
            {
                return (ValorInvestido - (ValorInvestido * 30) / 100);
            }
        }

        private bool VencimentoIgualOuMenorTresMeses() =>
            Utilities.DiferencaEmMeses(DateTime.Now, DataVencimento) <= 3;

        private bool PossuiTempoCustodiaSuperiorMetadeTempoDeInvestimento()
        {
            var tempoCustodiaTotalMeses = Utilities.DiferencaEmMeses(DataCompra, DataVencimento);
            var totalMesesInvestido = Utilities.DiferencaEmMeses(DataCompra, DateTime.Now);
            return totalMesesInvestido >= (tempoCustodiaTotalMeses / 2);
        }
        private double ObterValorRentabilidade() =>
            ValorTotal - ValorInvestido;

        private double ObterValorIr()
        {
            var valorRentabilidade = ObterValorRentabilidade();
            if (valorRentabilidade <= 0) return 0.00;
            return (valorRentabilidade - (valorRentabilidade * AliquotaIR) / 100);
        }
            
    }
}
