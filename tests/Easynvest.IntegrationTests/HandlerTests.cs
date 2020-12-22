using Easynvest.Domain.Handlers;
using Easynvest.Domain.Interfaces;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Easynvest.IntegrationTests
{
    public class HandlerTests
    {
        private readonly ITesouroDiretoHandler _tesouroDiretoHandler;
        private readonly IRendaFixaHandler _rendaFixaHandler;
        private readonly IFundoHandler _fundoHandler;
        private readonly INotificador _notificador;

        public HandlerTests()
        {
            _tesouroDiretoHandler = new TesouroDiretoHandler(_notificador);
            _rendaFixaHandler = new RendaFixaHandler(_notificador);
            _fundoHandler = new FundoHandler(_notificador);
        }

        [Fact(DisplayName = "Obter DTO de tesouro direto através do handler")]
        public async Task Obter_Tesouro_Direto_Atraves_do_Handler()
        {
            var entrypoint = "http://www.mocky.io/v2/5e3428203000006b00d9632a";
            var listaTesouroDireto = await _tesouroDiretoHandler.ObterTesouroDiretoAsync(entrypoint, new CancellationTokenSource().Token);
            Assert.True(listaTesouroDireto.Tds.Any());
        }

        [Fact(DisplayName = "Obter DTO de renda fixa através do handler")]
        public async Task Obter_Renda_Fixa_Atraves_do_Handler()
        {
            var entrypoint = "http://www.mocky.io/v2/5e3429a33000008c00d96336";
            var listaRendaFixa = await _rendaFixaHandler.ObterRendaFixaAsync(entrypoint, new CancellationTokenSource().Token);
            Assert.True(listaRendaFixa.Lcis.Any());
        }

        [Fact(DisplayName = "Obter DTO de fundos através do handler")]
        public async Task Obter_Fundos_Atraves_do_Handler()
        {
            var entrypoint = "http://www.mocky.io/v2/5e342ab33000008c00d96342";
            var listaFundos = await _fundoHandler.ObterFundosAsync(entrypoint, new CancellationTokenSource().Token);
            Assert.True(listaFundos.Fundos.Any());
        }
    }
}
