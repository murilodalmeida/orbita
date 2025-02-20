using FwksLabs.Libs.Serilog.Configuration;
using FwksLabs.Orbita.Core.Configuration.Settings;
using Serilog;
using Serilog.Core;
using SerilogLogger = FwksLabs.Libs.Serilog.Configuration.SerilogConfiguration;

namespace FwksLabs.Orbita.Web.Api.Configuration;

public static class SerilogConfiguration
{
    public static Logger CreateLogger(AppSettings appSettings)
    {
        var configuration = SerilogLogger
            .Default(appSettings.Telemetry)
            .WithOpenTelemetry(appSettings.Telemetry);

        if (appSettings.IsDevelopment)
            configuration.WriteTo.Console();

        return configuration.CreateLogger();
    }
}