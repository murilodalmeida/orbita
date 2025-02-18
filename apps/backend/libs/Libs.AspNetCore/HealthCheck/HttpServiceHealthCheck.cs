using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.HealthCheck.Arguments;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.AspNetCore.HealthCheck;

public class HttpServiceHealthCheck(Dictionary<string, HttpServiceHealthCheckArgs> args) : IHealthCheck
{
    private readonly Dictionary<string, HttpServiceHealthCheckArgs> args = args;

    public virtual async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        try
        {
            if (!args.TryGetValue(context.Registration.Name.Kebaberize(), out var serviceArgs))
                return Failure();

            var http = serviceArgs.HttpClientFactory.CreateClient();

            var response = await http.GetAsync(BuildUri(), cancellationToken);

            return response.IsSuccessStatusCode
                ? HealthCheckResult.Healthy()
                : Failure();

            Uri BuildUri()
            {
                var healthCheckPath = serviceArgs.HealthCheckPath.HasValue() ? serviceArgs.HealthCheckPath! : AppHealthCheck.EndpointsLiveness;

                return new($"{serviceArgs.ServiceUrl}/{healthCheckPath.TrimStart('/')}");
            }
        }
        catch (Exception exception)
        {
            return Failure(exception);
        }

        HealthCheckResult Failure(Exception? exception = default) => new(context.Registration.FailureStatus, AppErrorMessages.HealthCheckNotReachableError, exception);
    }
}