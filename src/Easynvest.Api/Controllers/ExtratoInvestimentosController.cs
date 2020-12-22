using AutoMapper;
using Easynvest.Api.Models;
using Easynvest.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/ExtratoInvestimentosController")]
    public class ExtratoInvestimentosController : MainController
    {
        private readonly IExtratoInvestimentoService _extratoInvestimentoService;
        private readonly IMapper _mapper;
        public ExtratoInvestimentosController(INotificador notificator,
                                              IMapper mapper,
                                              IExtratoInvestimentoService extratoInvestimentoService) : base(notificator)
        {
            _extratoInvestimentoService = extratoInvestimentoService;
            _mapper = mapper;
        }

        [HttpGet("ObterExtratoInvestimentos")]
        public async Task<ActionResult<ExtratoInvestimentoModel>> ObterExtratoInvestimentos(CancellationToken cancellationToken = default)
        {
            var extratoInvestimento = _mapper.Map<ExtratoInvestimentoModel>(await _extratoInvestimentoService.ConsolidarExtratoInvestimentos(cancellationToken));
            if(extratoInvestimento==null || extratoInvestimento.Investimentos.Count<=0)
            {
                Notificar("Não foi possível consolidar o extrato de investimentos, contate os administradores do sistema");
                return CustomResponse();
            }
            return CustomResponse(extratoInvestimento);
        }
    }
}
