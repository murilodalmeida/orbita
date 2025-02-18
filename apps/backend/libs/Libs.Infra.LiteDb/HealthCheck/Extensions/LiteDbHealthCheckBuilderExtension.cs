using System.Collections.Generic;
using FwksLabs.Libs.Core.HealthCheck.Extensions;
using FwksLabs.Libs.Infra.LiteDb.HealthCheck.Arguments;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.Infra.LiteDb.HealthCheck.Extensions;
public static class LiteDbHealthCheckBuilderExtension
{
    public static IServiceCollection AddLiteDbHealthCheck(this IServiceCollection services, string connectionString, string? name = default, int timeoutInSeconds = 10, IEnumerable<string>? tags = default)
    {
        return services
            .AddSingleton<LiteDbHealthCheckArgs>(_ => new(new LiteDatabase(connectionString)))
            .AddHealthChecks()
            .AddDatabaseCheck<LiteDbHealthCheck>(name, timeoutInSeconds, tags)
            .Services;
    }
}