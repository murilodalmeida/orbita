using System.Collections.Generic;
using System.Linq;
using FwksLabs.Libs.Core.Constants;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Orbita.Core.Configuration.Settings;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Sinks.Grafana.Loki;
using SerilogLogger = FwksLabs.Libs.Serilog.Configuration.SerilogConfiguration;

namespace FwksLabs.Orbita.Web.Api.Configuration;

public static class SerilogConfiguration
{
    public static Logger CreateLogger(AppSettings appSettings)
    {
        var configuration = SerilogLogger.Default(appSettings.Telemetry);

        var logLevel = appSettings.Telemetry.LoggingMinimumLevel.AsEnum<LogEventLevel>(LogEventLevel.Debug);

        ConfigureLoki();

        if (appSettings.IsDevelopment)
            configuration.WriteTo.Console();

        return configuration.CreateLogger();

        void ConfigureLoki()
        {
            configuration.WriteTo.GrafanaLoki(
                appSettings.Telemetry.LoggingExporterUrl,
                restrictedToMinimumLevel: logLevel,
                labels: Labels(),
                propertiesAsLabels: PropertyLabels());

            IEnumerable<LokiLabel> Labels() => [
                AppLogProperties.ServiceName.ToLokiLabel(appSettings.Telemetry.ServiceName),
                AppLogProperties.ServiceNamespace.ToLokiLabel(appSettings.Telemetry.ServiceNamespace),
                ..appSettings.Telemetry.LoggingLabels.Select(x => x.Key.ToLokiLabel(x.Value))
            ];

            IEnumerable<string> PropertyLabels() => [
                "level",
                ..appSettings.Telemetry.LoggingPropertyLabels
            ];
        }
    }
}

public static class LokiExtension
{
    public static LokiLabel ToLokiLabel(this string key, string value)
        => new() { Key = key, Value = value };
}