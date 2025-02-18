
using System;
using System.Collections.Generic;
using FwksLabs.Libs.Core.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenTelemetry;
using OpenTelemetry.Logs;
using OpenTelemetry.Resources;

namespace FwksLabs.Libs.Telemetry.Configuration;

public static class OpenTelemetryConfiguration
{
    public static void WithLogging(IServiceCollection services, OpenTelemetryBuilder builder, Action<LoggingOptions> optionsAction)
    {
        var options = new LoggingOptions();

        optionsAction.Invoke(options);

        builder.WithLogging(logging =>
        {
            logging.ConfigureResource(x => x
                .AddService(
                    options.ServiceName,
                    options.ServiceNamespace,
                    options.ServiceVersion,
                    options.ServiceInstanceId is null,
                    options.ServiceInstanceId)
                .AddAttributes(options.Attributes));

            logging.AddOtlpExporter(options =>
            {

            });
        });

        services.AddLogging(Configure);

        void Configure(ILoggingBuilder loggingBuilder)
        {
            if (options.Filters.Count > 0)
            {
                options.Filters.ForEach(pair =>
                    loggingBuilder.AddFilter(pair.Key, pair.Value));
            }

            loggingBuilder.AddOpenTelemetry(loggingOptions =>
            {
                loggingOptions.IncludeScopes = true;
                loggingOptions.IncludeFormattedMessage = true;
                loggingOptions.ParseStateValues = true;

                var resource = ResourceBuilder
                    .CreateDefault()
                    .AddService(
                        options.ServiceName,
                        options.ServiceNamespace,
                        options.ServiceVersion,
                        options.ServiceInstanceId is null,
                        options.ServiceInstanceId)
                    .AddAttributes(options.Attributes);

                loggingOptions.SetResourceBuilder(resource);
            });
        }
    }

    public sealed record LoggingOptions
    {
        public string ServiceName { get; set; } = $"Service_{Guid.NewGuid().AsString(7)}";
        public string? ServiceNamespace { get; set; }
        public string? ServiceVersion { get; set; }
        public string? ServiceInstanceId { get; set; }
        public List<KeyValuePair<string, object>> Attributes { get; set; } = [];
        public List<KeyValuePair<string, LogLevel>> Filters { get; set; } = [];
    }

    public sealed record OtlpExporterOptions();

    //public static OpenTelemetryBuilder ConfigureOpenTelemetry(this WebApplicationBuilder builder)
    //{
    //    builder.Logging.AddOpenTelemetry(o =>
    //    {
    //        o.IncludeFormattedMessage = true;
    //        o.IncludeScopes = true;
    //    });

    //    return builder.Services.AddOpenTelemetry();
    //}

    //public static OpenTelemetryBuilder ConfigureLoki(this OpenTelemetryBuilder builder, OpenTelemetrySettings settings)
    //{
    //    builder
    //        .Services
    //        .ConfigureOpenTelemetryLoggerProvider(logging => logging.AddOtlpExporter(ExporterOptions()));

    //    return builder;

    //    Action<OtlpExporterOptions> ExporterOptions() => options =>
    //    {
    //        options.Endpoint = new Uri(settings.Loki.Endpoint);
    //        options.Protocol = settings.Loki.Protocol.AsEnum<OtlpExportProtocol>(OtlpExportProtocol.HttpProtobuf);
    //    };
    //}

    //public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder, AppIdSettings appId, OpenTelemetrySettings settings)
    //{
    //    builder.Logging.AddOpenTelemetry(options =>
    //    {
    //        options.IncludeScopes = true;
    //        options.IncludeFormattedMessage = true;
    //    });

    //    builder.Services.AddOpenTelemetry()
    //        .WithMetrics(metrics =>
    //        {
    //            metrics.AddAspNetCoreInstrumentation();
    //            metrics.AddHttpClientInstrumentation();
    //            metrics.AddProcessInstrumentation();
    //            metrics.AddRuntimeInstrumentation()
    //                .AddMeter(
    //                    "Microsoft.AspNetCore.Hosting",
    //                    "Microsoft.AspNetCore.Server.Kestrel",
    //                    "System.Net.Http",
    //                    "FwksLabs.ResumeApp.Web.Api");
    //            metrics.AddPrometheusExporter();

    //            metrics.ConfigureResource(ConfigureService(appId));
    //        })
    //        .WithTracing(tracing =>
    //        {
    //            if (builder.Environment.IsDevelopment())
    //                tracing.SetSampler<AlwaysOnSampler>();

    //            tracing
    //                .AddAspNetCoreInstrumentation()
    //                .AddHttpClientInstrumentation();

    //            tracing.ConfigureResource(ConfigureService(appId));
    //        });

    //    builder.AddExporters(settings);

    //    return builder;

    //    static Action<ResourceBuilder> ConfigureService(AppIdSettings appId)
    //        => resource => resource
    //            .AddService(appId.Service.Kebaberize(), appId.GetFullName().Kebaberize(), appId.Version);
    //}

    //public static WebApplication UseOpenTelemetry(this WebApplication app)
    //{
    //    app
    //        .UseOpenTelemetryPrometheusScrapingEndpoint();

    //    return app;
    //}

    //private static void AddExporters(this IHostApplicationBuilder builder, OpenTelemetrySettings settings)
    //{
    //    builder.Services.ConfigureOpenTelemetryLoggerProvider(logging => logging.AddOtlpExporter(ExporterOptions()));
    //    builder.Services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter(ExporterOptions()));
    //    builder.Services.ConfigureOpenTelemetryTracerProvider(tracer => tracer.AddOtlpExporter(ExporterOptions()));

    //    Action<OtlpExporterOptions> ExporterOptions() => options =>
    //    {
    //        options.Endpoint = new Uri(settings.Endpoint);
    //        options.Protocol = settings.Protocol.AsEnum<OtlpExportProtocol>(OtlpExportProtocol.Grpc);
    //    };
    //}
}