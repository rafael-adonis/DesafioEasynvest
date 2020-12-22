using AutoMapper;
using Easynvest.Domain.Handlers;
using Easynvest.Domain.Interfaces;
using Easynvest.Domain.Mappers;
using Easynvest.Domain.Notifications;
using Easynvest.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Easynvest.Api.Configuration
{
    public static class DependencyInjectionConfiguration
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<IExtratoInvestimentoService, ExtratoInvestimentoService>();

            services.AddScoped<ITesouroDiretoHandler, TesouroDiretoHandler>();
            services.AddScoped<IRendaFixaHandler, RendaFixaHandler>();
            services.AddScoped<IFundoHandler, FundoHandler>();

            services.AddAutoMapper(typeof(AutoMapperDomainConfig).Assembly);
            services.AddScoped<INotificador, Notificador>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            return services;
        }
    }
}
