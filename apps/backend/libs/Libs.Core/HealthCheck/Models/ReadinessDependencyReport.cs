using System;
using System.Collections.Generic;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace FwksLabs.Libs.Core.HealthCheck.Models;

public sealed record ReadinessDependencyReport(string Name, TimeSpan Duration, HealthStatus Status, IEnumerable<string> Tags)
{
    public static ReadinessDependencyReport Transform(KeyValuePair<string, HealthReportEntry> entry)
        => new(entry.Key, entry.Value.Duration, entry.Value.Status, entry.Value.Tags);
}