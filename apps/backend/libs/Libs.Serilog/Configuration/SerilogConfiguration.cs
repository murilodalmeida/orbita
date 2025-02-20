using System.Collections.Generic;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Settings;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.OpenTelemetry;

namespace FwksLabs.Libs.Serilog.Configuration;

public static class SerilogConfiguration
{
    public static LoggerConfiguration Default(TelemetrySettings settings)
    {
        var configuration = new LoggerConfiguration();

        configuration
            .Enrich.FromLogContext()
            .Enrich.WithMachineName()
            .Enrich.WithEnvironmentName()
            .Enrich.WithClientIp()
            .Enrich.WithCorrelationId();

        configuration.MinimumLevel.Is(settings.LoggingMinimumLevel.AsEnum<LogEventLevel>(LogEventLevel.Debug));

        foreach (var conf in settings.LoggingMinimumLevelOverride)
            configuration.MinimumLevel.Override(conf.Key, conf.Value.AsEnum<LogEventLevel>());

        return configuration;
    }

    public static LoggerConfiguration WithOpenTelemetry(this LoggerConfiguration configuration, TelemetrySettings settings)
    {
        return configuration.WriteTo.OpenTelemetry(options =>
        {
            options.Endpoint = settings.CollectorEndpoint;
            options.Protocol = settings.CollectorProtocol.AsEnum<OtlpProtocol>(OtlpProtocol.Grpc);
            options.RestrictedToMinimumLevel = settings.LoggingMinimumLevel.AsEnum<LogEventLevel>(LogEventLevel.Debug);

            options.ResourceAttributes = Attributes();
        });

        IDictionary<string, object> Attributes()
        {
            settings.Attributes.Add("service.name", settings.ServiceName);
            settings.Attributes.Add("service.namespace", settings.ServiceNamespace);

            return settings.Attributes;
        }
    }

    //public static LoggerConfiguration WithGrafanaLoki(this LoggerConfiguration configuration, TelemetrySettings settings)
    //{
    //    var minimumLevel = settings.LoggingMinimumLevel.AsEnum<LogEventLevel>(LogEventLevel.Debug);

    //    AddDefaultLabels();

    //    return configuration.WriteTo.GrafanaLoki(
    //        settings.LoggingExporterUrl,
    //        restrictedToMinimumLevel: minimumLevel,
    //        labels: settings.LoggingLabels.Select(x => x.Key.ToLokiLabel(x.Value)),
    //        propertiesAsLabels: ["level", ..settings.LoggingPropertyLabels]);

    //    void AddDefaultLabels()
    //    {
    //        settings.LoggingLabels.Add(AppLogProperties.ServiceName.Underscore(), settings.ServiceName);
    //        settings.LoggingLabels.Add(AppLogProperties.ServiceNamespace.Underscore(), settings.ServiceNamespace);
    //    }
    //}
}
