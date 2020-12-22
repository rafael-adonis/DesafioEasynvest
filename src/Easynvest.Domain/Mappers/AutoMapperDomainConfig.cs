using AutoMapper;
using Easynvest.Domain.DTOs;
using Easynvest.Domain.Entities;
using Easynvest.Domain.Enums;

namespace Easynvest.Domain.Mappers
{
    public class AutoMapperDomainConfig : Profile
    {
        public AutoMapperDomainConfig()
        {
            CreateMap<Td, TesouroDireto>()
            .ForMember(td => td.DataVencimento, option => option.MapFrom(source => source.Vencimento))
            .ForMember(td => td.DataCompra, option => option.MapFrom(source => source.DataDeCompra))
            .ConstructUsing(td =>
                new TesouroDireto(
                    td.Indice,
                    (int)AliquotasImpostoDeRenda.TesouroDireto,
                    td.Iof,
                    td.Tipo,
                    td.Nome,
                    td.DataDeCompra,
                    td.Vencimento,
                    td.ValorInvestido,
                    td.ValorTotal)
            ).ReverseMap();

            CreateMap<Lci, RendaFixa>()
                .ForMember(lci => lci.ValorInvestido, option => option.MapFrom(source => source.CapitalInvestido))
                .ForMember(lci => lci.DataCompra, option => option.MapFrom(source => source.DataOperacao))
                .ForMember(lci => lci.DataVencimento, option => option.MapFrom(source => source.Vencimento))
                .ForMember(lci => lci.ValorTotal, option => option.MapFrom(source => source.CapitalAtual))
                .ConstructUsing(lci =>
                    new RendaFixa(
                        (int)AliquotasImpostoDeRenda.RendaFixa,
                        (int)lci.Iof,
                        lci.Tipo,
                        lci.Nome,
                        lci.DataOperacao,
                        lci.Vencimento,
                        lci.CapitalInvestido,
                        lci.CapitalAtual,
                        lci.Quantidade,
                        lci.OutrasTaxas,
                        lci.Taxas,
                        lci.Indice,
                        lci.GuarantidoFGC,
                        lci.PrecoUnitario,
                        lci.Primario)
                ).ReverseMap();

            CreateMap<Fundo, FundoInvestimento>()
                .ForMember(fundo => fundo.ValorInvestido, option => option.MapFrom(source => source.CapitalInvestido))
                .ForMember(fundo => fundo.ValorTotal, option => option.MapFrom(source => source.ValorAtual))
                .ForMember(fundo => fundo.DataVencimento, option => option.MapFrom(source => source.DataResgate))
                .ForMember(fundo => fundo.Quantidade, option => option.MapFrom(source => source.Quantity))
                .ConstructUsing(fundo =>
                    new FundoInvestimento(
                        (int)AliquotasImpostoDeRenda.Fundos,
                        fundo.Iof,
                        string.Empty,
                        fundo.Nome,
                        fundo.DataCompra,
                        fundo.DataResgate,
                        fundo.CapitalInvestido,
                        fundo.ValorAtual,
                        fundo.Quantity,
                        fundo.TotalTaxas)
                ).ReverseMap();
        }
    }
}
