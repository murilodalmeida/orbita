using FwksLabs.Libs.Telemetry.Configuration;
using FwksLabs.Orbita.Core.Configuration.Settings;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace FwksLabs.Orbita.Web.Api.Configuration;

public static class TelemetryConfiguration
{
    public static IHostApplicationBuilder ConfigureTelemetry(this IHostApplicationBuilder builder, AppSettings appSettings)
    {
        builder.Services
            .AddOpenTelemetry()
            .ConfigureTracing(appSettings.Telemetry)
            .ConfigureMetrics(appSettings.Telemetry);

        return builder;
    }
}