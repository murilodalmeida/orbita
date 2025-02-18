using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Core.HealthCheck.Models;

public sealed record ReadinessReport(HealthStatus Status, TimeSpan TotalDuration, IEnumerable<ReadinessDependencyReport> Dependencies)
{
    public static ReadinessReport From(HealthReport report)
    {
        var status = report.Status;

        if (report.Status == HealthStatus.Unhealthy)
        {
            status = HasCriticalUnhealthy() ? HealthStatus.Unhealthy : HealthStatus.Degraded;
        }

        return new(status, report.TotalDuration, report.Entries.Select(ReadinessDependencyReport.Transform));

        bool HasCriticalUnhealthy()
            => report.Entries.Any(x => x.Value.Tags.Contains(AppHealthCheck.TagsCritical) && x.Value.Status != HealthStatus.Healthy);
    }
}