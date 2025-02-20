using System.Collections.Generic;
using FwksLabs.Libs.Core.HealthCheck.Extensions;
using FwksLabs.Libs.Infra.Mongo.HealthCheck.Arguments;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.Mongo.HealthCheck.Extensions;

public static class MongoHealthCheckBuilderExtension
{
    public static IServiceCollection AddMongoHealthCheck(this IServiceCollection services, string connectionString, string? name = default, int timeoutInSeconds = 10, IEnumerable<string>? tags = default)
    {
        return services
            .AddSingleton<MongoHealthCheckArgs>(_ => new(new MongoClient(connectionString)))
            .AddHealthChecks()
            .AddDatabaseCheck<MongoHealthCheck>(name, timeoutInSeconds, tags)
            .Services;
    }
}