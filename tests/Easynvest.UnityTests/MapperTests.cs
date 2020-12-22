using AutoMapper;
using Easynvest.Domain.DTOs;
using Easynvest.Domain.Entities;
using Easynvest.Domain.Mappers;
using System;
using Xunit;

namespace Easynvest.UnityTests
{
    public class MapperTests
    {
        private readonly IMapper _mapper;
        public MapperTests()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(profile: new AutoMapperDomainConfig());
            });
            _mapper = mockMapper.CreateMapper();
        }


        [Fact(DisplayName = "Mapeamento do DTO de Tesouro Direto deve ser igual a entidade de Tesouro Direto.")]
        public void Teste_Mapeamento_TesouroDireto()
        {
            var tesouroDiretoDTO = new Td
            {
                ValorInvestido = 799.4720,
                ValorTotal = 829.68,
                Vencimento = Convert.ToDateTime("01/03/2025"),
                DataDeCompra = Convert.ToDateTime("01/03/2015"),
                Iof = 0,
                Indice = "SELIC",
                Tipo = "TD",
                Nome = "Tesouro Selic 2025"
            };

            var tesouroDiretoEntities = _mapper.Map<TesouroDireto>(tesouroDiretoDTO);
            Assert.Equal(tesouroDiretoDTO.ValorInvestido, tesouroDiretoEntities.ValorInvestido);
            Assert.Equal(tesouroDiretoDTO.ValorTotal, tesouroDiretoEntities.ValorTotal);
            Assert.Equal(tesouroDiretoDTO.Vencimento, tesouroDiretoEntities.DataVencimento);
            Assert.Equal(tesouroDiretoDTO.DataDeCompra, tesouroDiretoEntities.DataCompra);
            Assert.Equal(tesouroDiretoDTO.Iof, tesouroDiretoEntities.Iof);
            Assert.Equal(tesouroDiretoDTO.Indice, tesouroDiretoEntities.Indice);
            Assert.Equal(tesouroDiretoDTO.Nome, tesouroDiretoEntities.Nome);
        }

        [Fact(DisplayName = "Mapeamento do DTO de Renda Fixa deve ser igual a entidade de Renda Fixa.")]
        public void Teste_Mapeamento_Renda_Fixa()
        {
            var rendaFixaDTO = new Lci
            {
                CapitalInvestido = 2000.0,
                CapitalAtual = 2097.85,
                Quantidade = 2.00,
                Vencimento = Convert.ToDateTime("09/03/2021"),
                Iof = 0.0,
                OutrasTaxas = 0.0,
                Taxas = 0.0,
                Indice = "97% do CDI",
                Tipo = "LCI",
                Nome = "BANCO MAXIMA",
                GuarantidoFGC = true,
                DataOperacao = Convert.ToDateTime("14/03/2019"),
                PrecoUnitario = 1048.927450,
                Primario = false
            };

            var rendaFixaEntities = _mapper.Map<RendaFixa>(rendaFixaDTO);

            Assert.Equal(rendaFixaDTO.CapitalInvestido, rendaFixaEntities.ValorInvestido);
            Assert.Equal(rendaFixaDTO.CapitalAtual, rendaFixaEntities.ValorTotal);
            Assert.Equal(rendaFixaDTO.Quantidade, rendaFixaEntities.Quantidade);
            Assert.Equal(rendaFixaDTO.Vencimento, rendaFixaEntities.DataVencimento);
            Assert.Equal(rendaFixaDTO.Iof, rendaFixaEntities.Iof);
            Assert.Equal(rendaFixaDTO.OutrasTaxas, rendaFixaEntities.OutrasTaxas);
            Assert.Equal(rendaFixaDTO.Taxas, rendaFixaEntities.Taxas);
            Assert.Equal(rendaFixaDTO.Indice, rendaFixaEntities.Indice);
            Assert.Equal(rendaFixaDTO.Tipo, rendaFixaEntities.Tipo);
            Assert.Equal(rendaFixaDTO.Nome, rendaFixaEntities.Nome);
            Assert.Equal(rendaFixaDTO.GuarantidoFGC, rendaFixaEntities.GuarantidoFGC);
            Assert.Equal(rendaFixaDTO.DataOperacao, rendaFixaEntities.DataCompra);
            Assert.Equal(rendaFixaDTO.PrecoUnitario, rendaFixaEntities.PrecoUnitario);
            Assert.Equal(rendaFixaDTO.Primario, rendaFixaEntities.Primario);
        }

        [Fact(DisplayName = "Mapeamento do DTO de Fundos deve ser igual a entidade de Fundos.")]
        public void Teste_Mapeamento_Fundos()
        {
            var fundoDTO = new Fundo
            {
                CapitalInvestido = 1000,
                ValorAtual = 1159,
                DataResgate = Convert.ToDateTime("01/10/2022"),
                DataCompra = Convert.ToDateTime("15/11/2019"),
                Iof = 0,
                Nome = "ALASKA",
                TotalTaxas = 53.49,
                Quantity = 1
            };

            var fundoInvestimentoEntities = _mapper.Map<FundoInvestimento>(fundoDTO);
            Assert.Equal(fundoDTO.CapitalInvestido, fundoInvestimentoEntities.ValorInvestido);
            Assert.Equal(fundoDTO.ValorAtual, fundoInvestimentoEntities.ValorTotal);
            Assert.Equal(fundoDTO.DataResgate, fundoInvestimentoEntities.DataVencimento);
            Assert.Equal(fundoDTO.DataCompra, fundoInvestimentoEntities.DataCompra);
            Assert.Equal(fundoDTO.Iof, fundoInvestimentoEntities.Iof);
            Assert.Equal(fundoDTO.Nome, fundoInvestimentoEntities.Nome);
            Assert.Equal(fundoDTO.TotalTaxas, fundoInvestimentoEntities.TotalTaxas);
            Assert.Equal(fundoDTO.Quantity, fundoInvestimentoEntities.Quantidade);

        }
    }
}
