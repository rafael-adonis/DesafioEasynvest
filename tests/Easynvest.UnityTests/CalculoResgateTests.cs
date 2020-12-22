using Easynvest.Domain.Entities;
using Easynvest.Domain.Enums;
using System;
using Xunit;

namespace Easynvest.UnityTests
{
    public class CalculoResgateTests
    {

        [Fact(DisplayName = "Dado um investimento com menos de 3 meses para vencer, deve aplicar 6% de desconto no resgate")]
        public void Investimento_Com_Menos_3_Meses_Para_Vencimento_Deve_Perder_6_porcento_no_resgate()
        {
            var tesouroDireto = new TesouroDireto
            (
                indice: "SELIC",
                aliquotaIR: (double)AliquotasImpostoDeRenda.TesouroDireto,
                iof: 0,
                tipo: "TD",
                nome: "Tesouro Selic 2025",
                dataCompra: Convert.ToDateTime("01/03/2015"),
                dataVencimento: Convert.ToDateTime("01/11/2020"),
                valorInvestido: 799.4720,
                valorTotal: 829.68
            );

            Assert.True(tesouroDireto.ValorResgate == (tesouroDireto.ValorInvestido - (tesouroDireto.ValorInvestido * 6) / 100));
        }

        [Fact(DisplayName = "Dado um investimento com mais da metade do tempo de custódia, deve aplicar 15% de desconto no resgate")]
        public void Investimento_Com_Mais_Da_Metade_Do_Tempo_De_Custodia_Deve_Aplicar_15_porcento_no_resgate()
        {
            var tesouroDireto = new TesouroDireto
            (
                indice: "SELIC",
                aliquotaIR: (double)AliquotasImpostoDeRenda.TesouroDireto,
                iof: 0,
                tipo: "TD",
                nome: "Tesouro Selic 2025",
                dataCompra: Convert.ToDateTime("01/06/2015"),
                dataVencimento: Convert.ToDateTime("01/11/2021"),
                valorInvestido: 799.4720,
                valorTotal: 829.68
            );

            Assert.True(tesouroDireto.ValorResgate == (tesouroDireto.ValorInvestido - (tesouroDireto.ValorInvestido * 15) / 100));
        }

        [Fact(DisplayName = "Dado um investimento que não possui mais da metade do tempo de custódia e também não possui 3 ou menos meses para o vencimento, deve aplicar 30% de desconto no resgate")]
        public void Investimento_Sem_Metade_Do_Tempo_De_Custodia_E_Nao_Possui_3_Meses_Ou_Menos_Para_Vencimento_Deve_Aplicar_30_porcendo_no_resgate()
        {
            var tesouroDireto = new TesouroDireto
            (
                indice: "SELIC",
                aliquotaIR: (double)AliquotasImpostoDeRenda.TesouroDireto,
                iof: 0,
                tipo: "TD",
                nome: "Tesouro Selic 2025",
                dataCompra: Convert.ToDateTime("01/06/2019"),
                dataVencimento: Convert.ToDateTime("01/11/2025"),
                valorInvestido: 799.4720,
                valorTotal: 829.68
            );

            Assert.True(tesouroDireto.ValorResgate == (tesouroDireto.ValorInvestido - (tesouroDireto.ValorInvestido * 30) / 100));
        }
    }
}
