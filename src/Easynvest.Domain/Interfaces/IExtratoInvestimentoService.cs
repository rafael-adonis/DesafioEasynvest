using Easynvest.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Domain.Interfaces
{
    public interface IExtratoInvestimentoService
    {
        Task<IEnumerable<TesouroDireto>> ObterInvestimentosTesouroDireto(CancellationToken cancellationToken);
        Task<IEnumerable<RendaFixa>> ObterInvestimentosRendaFixa(CancellationToken cancellationToken);
        Task<IEnumerable<FundoInvestimento>> ObterInvestimentosFundos(CancellationToken cancellationToken);
        Task<ExtratoInvestimentos> ConsolidarExtratoInvestimentos(CancellationToken cancellationToken);
    }
}
