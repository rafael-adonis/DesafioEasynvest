using Easynvest.Domain.Entities.Core;
using Easynvest.Domain.Interfaces;
using Easynvest.Domain.Notifications;
using Easynvest.Infrastructure.Utils;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Options;

namespace Easynvest.Domain.Services
{
    public abstract class BaseService
    {
        protected readonly IOptions<EnvironmentSetting> _options;
        private readonly INotificador _notificador;        
        protected BaseService(IOptions<EnvironmentSetting> options, INotificador notificador)
        {
            _notificador = notificador;
            _options = options;
        }

        protected void Notificar(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notificar(error.ErrorMessage);
            }
        }

        protected void Notificar(string mensagem)
        {
            _notificador.Handle(new Notificacao(mensagem));
        }

        protected bool ExecutarValidacao<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE> where TE : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notificar(validator);

            return false;
        }
    }
}
