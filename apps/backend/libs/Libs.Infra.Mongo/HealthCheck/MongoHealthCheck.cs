using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Infra.Mongo.HealthCheck.Arguments;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.Mongo.HealthCheck;

public sealed class MongoHealthCheck(MongoHealthCheckArgs args) : IHealthCheck
{
    private readonly MongoHealthCheckArgs args = args;

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            using var cursor = await args.MongoClient.ListDatabaseNamesAsync(cancellationToken);

            _ = await cursor.FirstOrDefaultAsync(cancellationToken);

            return HealthCheckResult.Healthy();
        }
        catch (Exception exception)
        {
            return new HealthCheckResult(context.Registration.FailureStatus, AppErrorMessages.HealthCheckNotReachableError, exception);
        }
    }
}