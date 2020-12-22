using Easynvest.Domain.DTOs;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Domain.Interfaces
{
    public interface ITesouroDiretoHandler
    {
        Task<TesouroDiretoDTO> ObterTesouroDiretoAsync(string entrypoint, CancellationToken cancellationToken);

    }
}
