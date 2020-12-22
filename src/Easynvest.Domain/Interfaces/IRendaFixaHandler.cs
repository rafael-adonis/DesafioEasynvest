using Easynvest.Domain.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Domain.Interfaces
{
    public interface IRendaFixaHandler
    {
        Task<RendaFixaDTO> ObterRendaFixaAsync(string entrypoint, CancellationToken cancellationToken);
    }
}
