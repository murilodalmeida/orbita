using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Infra.LiteDb.HealthCheck.Arguments;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Infra.LiteDb.HealthCheck;

public sealed class LiteDbHealthCheck(LiteDbHealthCheckArgs args) : IHealthCheck
{
    private readonly LiteDbHealthCheckArgs args = args;

    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            _ = args.LiteDatabase.GetCollectionNames();

            return Task.FromResult(HealthCheckResult.Healthy());
        }
        catch (Exception exception)
        {
            return Task.FromResult(new HealthCheckResult(context.Registration.FailureStatus, AppErrorMessages.HealthCheckNotReachableError, exception));
        }
    }
}