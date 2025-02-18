using System;
using System.Collections.Generic;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Core.HealthCheck.Extensions;

public static class HealthChecksBuilderExtension
{
    public static IHealthChecksBuilder AddHttpServiceCheck<THealthCheck>(
        this IHealthChecksBuilder builder, string name, HealthStatus failureStatus = HealthStatus.Unhealthy, int timeoutInSeconds = AppHealthCheck.TimeoutInSeconds, IEnumerable<string>? tags = default)
            where THealthCheck : class, IHealthCheck
    {
        tags = [.. tags is null ? [] : tags, AppHealthCheck.TagsReadiness, AppHealthCheck.TagsTypeHttpService];

        return builder.AddCheck<THealthCheck>(name, failureStatus, tags, TimeSpan.FromSeconds(timeoutInSeconds));
    }

    public static IHealthChecksBuilder AddDatabaseCheck<THealthCheck>(
        this IHealthChecksBuilder builder, string? name = default, int timeoutInSeconds = AppHealthCheck.TimeoutInSeconds, IEnumerable<string>? tags = default)
            where THealthCheck : class, IHealthCheck
    {
        name = name is not null && name.HasValue() ? name : typeof(THealthCheck).Name.Replace("HealthCheck", string.Empty).ToLowerInvariant();

        tags = [.. tags is null ? [] : tags, AppHealthCheck.TagsReadiness, AppHealthCheck.TagsTypeDatabase];

        return builder.AddCheck<THealthCheck>(name, HealthStatus.Unhealthy, tags, TimeSpan.FromSeconds(timeoutInSeconds));
    }

    public static IHealthChecksBuilder AddLivenessCheck(this IHealthChecksBuilder builder)
        => builder.AddCheck("liveness", () => HealthCheckResult.Healthy(), [AppHealthCheck.TagsLiveness], TimeSpan.FromSeconds(5));
}