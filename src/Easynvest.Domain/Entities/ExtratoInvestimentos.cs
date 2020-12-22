using System.Collections.Generic;
using System.Linq;

namespace Easynvest.Domain.Entities
{
    public class ExtratoInvestimentos 
    {
        public ExtratoInvestimentos()
        {            
            Investimentos = new List<Investimento>();
        }

        public double ValorTotal => Investimentos.Sum(x => x.ValorTotal);
        
        public List<Investimento> Investimentos { get; private set; }

        public void AdicionarInvestimento(Investimento investimento)
        {
            Investimentos.Add(investimento);
        }

        public void AdicionarRangeInvestimentos(List<Investimento> investimentos)
        {
            Investimentos.AddRange(investimentos);
        }
    }
}
