using FwksLabs.Orbita.Core.Configuration.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using Serilog;

namespace FwksLabs.Orbita.Web.Api.Configuration;

public static class TelemetryConfiguration
{
    public static IHostApplicationBuilder ConfigureTelemetry(this IHostApplicationBuilder builder, AppSettings appSettings)
    {
        builder.Services
            .AddSerilog()
            .AddOpenTelemetry()
            .ConfigureTracing(appSettings)
            .ConfigureMetrics(appSettings);

        return builder;
    }

    private static OpenTelemetryBuilder ConfigureTracing(this OpenTelemetryBuilder builder, AppSettings appSettings)
    {
        return builder;
    }

    private static OpenTelemetryBuilder ConfigureMetrics(this OpenTelemetryBuilder builder, AppSettings appSettings)
    {
        return builder;
    }
}