using Easynvest.Domain.DTOs;
using Easynvest.Domain.Interfaces;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Easynvest.Domain.Handlers
{
    public class RendaFixaHandler : RequestHandler, IRendaFixaHandler
    {
        public RendaFixaHandler(INotificador notificador, string proxy = null, string proxyUser = null, string proxyPassword = null) : base(notificador, proxy, proxyUser, proxyPassword)
        {
        }

        public async Task<RendaFixaDTO> ObterRendaFixaAsync(string entrypoint, CancellationToken cancellationToken)
        {
            var httpResponseMessage = await GetAsync(entrypoint, cancellationToken);
            string response = await httpResponseMessage.Content.ReadAsStringAsync();

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                Notificar($"Falha ao obter as informações de renda fixa. HttpResponseMessage:{httpResponseMessage}");
                return null;
            }

            return JsonConvert.DeserializeObject<RendaFixaDTO>(response);
        }
    }
}
