using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Infra.Postgres.HealthCheck.Arguments;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Infra.Postgres.HealthCheck;

public sealed class PostgresHealthCheck(PostgresHealthCheckArgs args) : IHealthCheck
{
    private readonly PostgresHealthCheckArgs args = args;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            await using var command = args.Datasource.CreateCommand("SELECT 1;");

            await command.ExecuteNonQueryAsync(cancellationToken);

            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, AppErrorMessages.HealthCheckNotReachableError, exception);
        }
    }
}