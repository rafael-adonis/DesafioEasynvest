using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Net.Mime;

namespace Easynvest.Api.Configuration
{
    public static class HealthCheckConfiguration
    {
        public static IServiceCollection AddHealthCheckConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks();

            services.AddHealthChecksUI(options =>
            {
                options.SetEvaluationTimeInSeconds(Convert.ToInt32(configuration.GetSection("HealthChecks-UI:EvaluationTimeOnSeconds").Value));
                options.SetMinimumSecondsBetweenFailureNotifications(Convert.ToInt32(configuration.GetSection("HealthChecks-UI:MinimumSecondsBetweenFailureNotifications").Value));
                options.MaximumHistoryEntriesPerEndpoint(Convert.ToInt32(configuration.GetSection("HealthChecks-UI:MaximumExecutionHistoriesPerEndpoint").Value));

            }).AddInMemoryStorage();
            return services;
        }

        public static IApplicationBuilder UseHealthCheckConfiguration(this IApplicationBuilder app)
        {
            app.UseHealthChecks("/status",
               new HealthCheckOptions()
               {
                   ResponseWriter = async (context, report) =>
                   {
                       var result = JsonConvert.SerializeObject(
                           new
                           {
                               statusApplication = report.Status.ToString(),
                               healthChecks = report.Entries.Select(e => new
                               {
                                   check = e.Key,
                                   ErrorMessage = e.Value.Exception?.Message,
                                   status = Enum.GetName(typeof(HealthStatus), e.Value.Status)
                               })
                           });
                       context.Response.ContentType = MediaTypeNames.Application.Json;
                       await context.Response.WriteAsync(result);
                   }
               });

            app.UseHealthChecks("/healthchecks-data-ui", new HealthCheckOptions()
            {
                Predicate = _ => true,
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });

            app.UseHealthChecksUI(options =>
            {
                options.UIPath = "/healthchecks-ui";
                options.ApiPath = "/health-ui-api";
            });

            return app;
        }
    }
}
