using System.Collections.Generic;
using FwksLabs.Libs.Core.HealthCheck.Extensions;
using FwksLabs.Libs.Infra.Postgres.HealthCheck.Arguments;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace FwksLabs.Libs.Infra.Postgres.HealthCheck.Extensions;
public static class PostgresHealthCheckBuilderExtension
{
    public static IServiceCollection AddPostgresHealthCheck(this IServiceCollection services, string connectionString, string databaseName = "postgres", string? name = default, int timeoutInSeconds = 10, IEnumerable<string>? tags = default)
    {
        return services
            .AddSingleton<PostgresHealthCheckArgs>(_ => new(NpgsqlDataSource.Create(new NpgsqlConnectionStringBuilder(connectionString) { Database = databaseName })))
            .AddHealthChecks()
            .AddDatabaseCheck<PostgresHealthCheck>(name, timeoutInSeconds, tags)
            .Services;
    }
}