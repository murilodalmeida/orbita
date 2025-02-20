using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using FwksLabs.Libs.AspNetCore.HealthCheck.Arguments;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.HealthCheck.Extensions;
using FwksLabs.Libs.Core.HealthCheck.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.HealthCheck.Extensions;

public static class HttpHealthCheckBuilderExtension
{
    public static IServiceCollection AddHttpServicesHealthCheck(this IServiceCollection services, IDictionary<string, HttpServiceDependencySettings> httpServices)
    {
        services
            .AddSingleton(sp => BuildServiceDependenciesDictionary(sp, httpServices));

        var healthChecksBuilder = services.AddHealthChecks();

        foreach (var service in httpServices)
            healthChecksBuilder.AddHttpServiceCheck<HttpServiceHealthCheck>(
                GetName(service),
                failureStatus: service.Value.Critical ? HealthStatus.Unhealthy : HealthStatus.Degraded,
                service.Value.Timeout,
                tags: [service.Value.Critical ? AppHealthCheck.TagsCritical : AppHealthCheck.TagsNonCritical]);

        return services;
    }

    private static Dictionary<string, HttpServiceHealthCheckArgs> BuildServiceDependenciesDictionary(IServiceProvider serviceProvider, IDictionary<string, HttpServiceDependencySettings> services)
    {
        return services.ToDictionary(
            GetName,
            service => new HttpServiceHealthCheckArgs(
                serviceProvider.GetRequiredService<IHttpClientFactory>(),
                GetName(service),
                service.Value.Url,
                service.Value.HealthCheckPath
            )
        );
    }
    private static string GetName(KeyValuePair<string, HttpServiceDependencySettings> service)
            => service.Value.Label ?? service.Key;
}