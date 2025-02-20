using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace FwksLabs.Libs.Core.Settings;

public record TelemetrySettings(
    string ServiceName,
    string ServiceNamespace,
    string ServiceVersion,
    string ServiceInstanceId,
    string CollectorEndpoint,
    string CollectorProtocol,
    string LoggingMinimumLevel,
    string TemporalityPreference
)
{
    public Dictionary<string, object> Attributes { get; set; } = [];
    public Dictionary<string, string> LoggingMinimumLevelOverride { get; set; } = [];
    public IEnumerable<ActivitySource> ActivitySources { get; set; } = [];
    public IEnumerable<string> PathsFilter { get; set; } = [];
    public Func<HttpRequestMessage, bool>? HttpRequestFilter { get; set; }
    // public Func<HttpContext, bool>? HttpRequestFilter { get; set; }
}