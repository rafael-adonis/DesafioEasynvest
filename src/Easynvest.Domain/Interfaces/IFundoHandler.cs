using Easynvest.Domain.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Domain.Interfaces
{
    public interface IFundoHandler
    {
        Task<FundoDTO> ObterFundosAsync(string entrypoint, CancellationToken cancellationToken);
    }
}
