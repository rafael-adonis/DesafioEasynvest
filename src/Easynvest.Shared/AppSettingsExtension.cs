using Easynvest.Domain.Entities.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Easynvest.Shared
{
    public static class AppSettingsExtension
    {
        public static IServiceCollection AddEnvironmentSettings(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EnvironmentSetting>(options =>
            {
                options.TesouroDiretoEndPoint = configuration.GetSection("App:TesouroDiretoEndPoint").Value;
                options.RendaFixaEndPoint = configuration.GetSection("App:RendaFixaEndPoint").Value;
                options.FundosEndPoint = configuration.GetSection("App:FundosEndPoint").Value;
            });
            return services;
        }
    }
}
