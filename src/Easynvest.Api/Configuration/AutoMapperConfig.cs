using AutoMapper;
using Easynvest.Api.Models;
using Easynvest.Domain.Entities;

namespace Easynvest.Api.Configuration
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<ExtratoInvestimentoModel, ExtratoInvestimentos>().ReverseMap();
            CreateMap<InvestimentoModel, Investimento>().ReverseMap();

            
        }
    }
}
