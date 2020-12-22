using System;
using System.Collections.Generic;

namespace Easynvest.Domain.DTOs
{

    public class TesouroDiretoDTO
    {
        public List<Td> Tds { get; set; }
    }
    public class Td
    {
        public double ValorInvestido { get; set; }
        public double ValorTotal { get; set; }
        public DateTime Vencimento { get; set; }
        public DateTime DataDeCompra { get; set; }
        public int Iof { get; set; }
        public string Indice { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
    }
}
