using System.Collections.Generic;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.HealthCheck.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.HealthCheck.Configuration;

public static class HealthCheckEndpointsConfiguration
{
    private static readonly Dictionary<HealthStatus, int> resultStatusCodes = new()
    {
        [HealthStatus.Healthy] = StatusCodes.Status200OK,
        [HealthStatus.Degraded] = StatusCodes.Status200OK,
        [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
    };

    public static IApplicationBuilder UseHealthCheckEndpoints(this IApplicationBuilder appBuilder)
    {
        return appBuilder
            .UseLivenessEndpoint()
            .UseReadinessEndpoint();
    }

    private static IApplicationBuilder UseLivenessEndpoint(this IApplicationBuilder appBuilder)
    {
        return appBuilder
            .UseHealthChecks(AppHealthCheck.EndpointsLiveness, new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(AppHealthCheck.TagsLiveness),
                AllowCachingResponses = false,
                ResultStatusCodes = resultStatusCodes,
                ResponseWriter = async (context, report) => await context.Response.WriteAsJsonAsync(new { report.Status })
            });
    }

    private static IApplicationBuilder UseReadinessEndpoint(this IApplicationBuilder appBuilder)
    {
        return appBuilder
            .UseHealthChecks(AppHealthCheck.EndpointsReadiness, new HealthCheckOptions
            {
                Predicate = check => check.Tags.Contains(AppHealthCheck.TagsReadiness),
                AllowCachingResponses = false,
                ResultStatusCodes = resultStatusCodes,
                ResponseWriter = async (context, report) => await context.Response.WriteAsJsonAsync(ReadinessReport.From(report))
            });
    }
}