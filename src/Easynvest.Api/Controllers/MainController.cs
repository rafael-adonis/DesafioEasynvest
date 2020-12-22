using Easynvest.Domain.Interfaces;
using Easynvest.Domain.Notifications;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;

namespace Easynvest.Api.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        public MainController(INotificador notificator)
        {
            _notificador = notificator;
        }

        protected bool OperacaoValida() => !_notificador.TemNotificacao();
        protected ActionResult CustomResponse(object result = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = result
                });
            }
            return BadRequest(new
            {
                success = false,
                errors = _notificador.ObterNotificacoes().Select(n => n.Mensagem)
            });
        }

        protected ActionResult ActionResult(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarModelInvalida(modelState);
            return CustomResponse();
        }

        private void NotificarModelInvalida(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                var errorMessage = error.Exception == null ? error.ErrorMessage : error.Exception.Message;
                Notificar(errorMessage);
            }

        }
        protected void Notificar(string errorMessage)
        {
            _notificador.Handle(new Notificacao(errorMessage));
        }
    }
}
