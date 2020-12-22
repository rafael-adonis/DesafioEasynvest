using System;

namespace Easynvest.Domain.Entities
{
    public class Investimento
    {
        public Investimento(string nome, double valorInvestido, double valorTotal, DateTime vencimento, double ir, double valorResgate)
        {
            Nome = nome;
            ValorInvestido = valorInvestido;
            ValorTotal = valorTotal;
            Vencimento = vencimento;
            Ir = ir;
            ValorResgate = valorResgate;
        }

        public string Nome { get; private set; }
        public double ValorInvestido { get; private set; }
        public double ValorTotal { get; private set; }
        public DateTime Vencimento { get; private set; }
        public double Ir { get; private set; }
        public double ValorResgate { get; private set; }
    }
}
