using System;
using System.Collections.Generic;

namespace Easynvest.Domain.DTOs
{
    public class FundoDTO
    {
        public List<Fundo> Fundos { get; set; }
    }
    public class Fundo
    {
        public double CapitalInvestido { get; set; }
        public double ValorAtual { get; set; }
        public DateTime DataResgate { get; set; }
        public DateTime DataCompra { get; set; }
        public int Iof { get; set; }
        public string Nome { get; set; }
        public double TotalTaxas { get; set; }
        public int Quantity { get; set; }
    }

}
