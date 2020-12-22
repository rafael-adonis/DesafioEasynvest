using AutoMapper;
using Easynvest.Domain.Entities;
using Easynvest.Domain.Interfaces;
using Easynvest.Infrastructure.Utils;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Domain.Services
{
    public class ExtratoInvestimentoService : BaseService, IExtratoInvestimentoService
    {
        private readonly ITesouroDiretoHandler _tesouroDiretoHandler;
        private readonly IRendaFixaHandler _rendaFixaHandler;
        private readonly IFundoHandler _fundoHandler;
        private readonly IMapper _mapper;

        public ExtratoInvestimentoService(IOptions<EnvironmentSetting> options,
               INotificador notificador,
               IMapper mapper,
               ITesouroDiretoHandler tesouroDiretoHandler,
               IRendaFixaHandler rendaFixaHandler,
               IFundoHandler fundoHandler) : base(options, notificador)
        {
            _mapper = mapper;
            _tesouroDiretoHandler = tesouroDiretoHandler;
            _rendaFixaHandler = rendaFixaHandler;
            _fundoHandler = fundoHandler;
        }

        public async Task<ExtratoInvestimentos> ConsolidarExtratoInvestimentos(CancellationToken cancellationToken)
        {
            var extratoInvestimento = new ExtratoInvestimentos();            
            var tesouroDireto = await ObterInvestimentosTesouroDireto(cancellationToken);
            if (tesouroDireto.Where(x => !x.IsValid).Any())
            {
                Notificar("As informações de tesouro direto estão inconsistentes");
                return null;
            }                
            ConsolidarExtratoTesouroDireto(extratoInvestimento, tesouroDireto);

            var rendaFixa = await ObterInvestimentosRendaFixa(cancellationToken);
            if (rendaFixa.Where(x => !x.IsValid).Any())
            {
                Notificar("As informações de renda fixa estão inconsistentes");
                return null;
            }
            ConsolidarExtratoRendaFixa(extratoInvestimento, rendaFixa);

            var fundos = await ObterInvestimentosFundos(cancellationToken);
            if (fundos.Where(x => !x.IsValid).Any())
            {
                Notificar("As informações de invesimento em fundos estão inconsistentes");
                return null;
            }

            ConsolidarExtratoFundo(extratoInvestimento, fundos);

            return extratoInvestimento;
        }

        public async Task<IEnumerable<TesouroDireto>> ObterInvestimentosTesouroDireto(CancellationToken cancellationToken)
        {
            var tesouroDiretoDto = await _tesouroDiretoHandler.ObterTesouroDiretoAsync(_options.Value.TesouroDiretoEndPoint, cancellationToken);
            if (tesouroDiretoDto == null) return null;
            return _mapper.Map<List<TesouroDireto>>(tesouroDiretoDto.Tds);
        }

        public async Task<IEnumerable<RendaFixa>> ObterInvestimentosRendaFixa(CancellationToken cancellationToken)
        {
            var rendaFixa = await _rendaFixaHandler.ObterRendaFixaAsync(_options.Value.RendaFixaEndPoint, cancellationToken);
            if (rendaFixa == null) return null;
            return _mapper.Map<List<RendaFixa>>(rendaFixa.Lcis);
        }

        public async Task<IEnumerable<FundoInvestimento>> ObterInvestimentosFundos(CancellationToken cancellationToken)
        {
            var fundos = await _fundoHandler.ObterFundosAsync(_options.Value.FundosEndPoint, cancellationToken);
            if (fundos == null) return null;
            return _mapper.Map<List<FundoInvestimento>>(fundos.Fundos);
        }

        private ExtratoInvestimentos ConsolidarExtratoTesouroDireto(ExtratoInvestimentos extratoInvestimento, IEnumerable<TesouroDireto> investimentosTesouroDireto)
        {
            var listaInvestimentos = new List<Investimento>();
            investimentosTesouroDireto.ToList().ForEach(delegate (TesouroDireto tesouroDireto)
            {
                var investimento = new Investimento
                (
                    nome : tesouroDireto.Nome,
                    valorInvestido : tesouroDireto.ValorInvestido,
                    valorTotal : tesouroDireto.ValorTotal,
                    vencimento : tesouroDireto.DataVencimento,
                    ir : tesouroDireto.ValorIr,
                    valorResgate : tesouroDireto.ValorResgate
                );
                listaInvestimentos.Add(investimento);
            });
            extratoInvestimento.AdicionarRangeInvestimentos(listaInvestimentos);
            return extratoInvestimento;
        }

        private ExtratoInvestimentos ConsolidarExtratoRendaFixa(ExtratoInvestimentos extratoInvestimento, IEnumerable<RendaFixa> investimentosRendaFixa)
        {
            var listaInvestimentos = new List<Investimento>();
            investimentosRendaFixa.ToList().ForEach(delegate (RendaFixa rendaFixa)
            {
                var investimento = new Investimento
                (
                    nome : rendaFixa.Nome,
                    valorInvestido : rendaFixa.ValorInvestido,
                    valorTotal : rendaFixa.ValorTotal,
                    vencimento : rendaFixa.DataVencimento,
                    ir : rendaFixa.ValorIr,
                    valorResgate : rendaFixa.ValorResgate
                );
                listaInvestimentos.Add(investimento);
            });
            
            extratoInvestimento.AdicionarRangeInvestimentos(listaInvestimentos);
            return extratoInvestimento;
        }

        private ExtratoInvestimentos ConsolidarExtratoFundo(ExtratoInvestimentos extratoInvestimento, IEnumerable<FundoInvestimento> fundosInvestimento)
        {
            var listaInvestimentos = new List<Investimento>();
            fundosInvestimento.ToList().ForEach(delegate (FundoInvestimento fundoInvestimento)
            {
                var investimento = new Investimento
                (
                    nome : fundoInvestimento.Nome,
                    valorInvestido : fundoInvestimento.ValorInvestido,
                    valorTotal : fundoInvestimento.ValorTotal,
                    vencimento : fundoInvestimento.DataVencimento,
                    ir : fundoInvestimento.ValorIr,
                    valorResgate : fundoInvestimento.ValorResgate
                );
                listaInvestimentos.Add(investimento);
            });            
            extratoInvestimento.AdicionarRangeInvestimentos(listaInvestimentos);
            return extratoInvestimento;
        }
    }
}
