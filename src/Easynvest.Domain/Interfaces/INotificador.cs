using Easynvest.Domain.Notifications;
using System.Collections.Generic;

namespace Easynvest.Domain.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
