using Easynvest.Domain.Interfaces;
using Easynvest.Domain.Notifications;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Easynvest.Domain.Handlers
{
    public abstract class RequestHandler
    {
        private static string _proxy;
        private static string _proxyUser;
        private static string _proxyPassword;
        private readonly INotificador _notificador;

        protected static JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings() { Culture = new System.Globalization.CultureInfo("pt-BR") };

        public RequestHandler(INotificador notificador, string proxy = null, string proxyUser = null, string proxyPassword = null)
        {
            _proxy = proxy;
            _proxyUser = proxyUser;
            _proxyPassword = proxyPassword;
            _notificador = notificador;
        }

        public void Notificar(string mensagem) =>
            _notificador.Handle(new Notificacao(mensagem));

        public static async Task<HttpResponseMessage> GetAsync(string entryPoint, CancellationToken cancellationToken, List<KeyValuePair<string, string>> header = null, List<KeyValuePair<string, string>> requestParams = null, bool useProxy = false)
        {
            HttpClientHandler httpClientHandler = new HttpClientHandler
            {
                UseProxy = true,
                Proxy = new WebProxy(_proxy, false, new string[] { }, new NetworkCredential(_proxyUser, _proxyPassword))
            };
            using (HttpClient httpClient = useProxy ? new HttpClient(httpClientHandler) : new HttpClient())
            {
                if (header != null)
                    foreach (KeyValuePair<string, string> item in header)
                        httpClient.DefaultRequestHeaders.Add(item.Key, item.Value);

                if (requestParams != null && requestParams.Any())
                {
                    NameValueCollection queryString = HttpUtility.ParseQueryString(string.Empty);

                    foreach (KeyValuePair<string, string> param in requestParams)
                        queryString[param.Key] = param.Value;

                    entryPoint = $"{entryPoint}?{queryString}";
                }

                return await httpClient.GetAsync(entryPoint, cancellationToken);
            }
        }
    }
}
