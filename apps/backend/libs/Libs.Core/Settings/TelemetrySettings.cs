using System.Collections.Generic;

namespace FwksLabs.Libs.Core.Settings;

public record TelemetrySettings(
    string ServiceName,
    string ServiceNamespace,
    string ServiceVersion,
    string LoggingExporterUrl,
    string LoggingMinimumLevel,
    IDictionary<string, string> LoggingMinimumLevelOverride,
    IDictionary<string, string> LoggingLabels,
    IEnumerable<string> LoggingPropertyLabels
);

public record TelemetryLoggingLabel(string Display, bool IsProperty);