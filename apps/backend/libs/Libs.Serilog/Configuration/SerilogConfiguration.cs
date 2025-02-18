using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Settings;
using Serilog;
using Serilog.Events;

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
}