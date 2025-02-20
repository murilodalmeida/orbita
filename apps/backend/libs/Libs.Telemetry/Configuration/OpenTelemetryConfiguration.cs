using System;
using System.Diagnostics;
using System.Linq;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Settings;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Instrumentation.AspNetCore;
using OpenTelemetry.Instrumentation.Http;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace FwksLabs.Libs.Telemetry.Configuration;

public static class OpenTelemetryConfiguration
{
    public static OpenTelemetryBuilder ConfigureTracing(this OpenTelemetryBuilder builder, TelemetrySettings settings)
    {
        return builder
            .WithTracing(builder => builder

                .ConfigureResource(resource => resource
                    .AddService(
                        settings.ServiceName,
                        settings.ServiceNamespace,
                        settings.ServiceVersion,
                        settings.ServiceInstanceId is null,
                        settings.ServiceInstanceId)
                    .AddAttributes(settings.Attributes))

                .AddOtlpExporter(options =>
                {
                    options.Endpoint = new Uri($"{settings.CollectorEndpoint}/v1/traces");
                    options.Protocol = settings.CollectorProtocol.AsEnum<OtlpExportProtocol>(OtlpExportProtocol.Grpc);
                })

                .AddSource(settings.ActivitySources.Select(x => x.Name).ToArray())

                .AddHttpClientInstrumentation(HttpClientInstrumentationFilter())

                .AddAspNetCoreInstrumentation(AddAspNetCoreInstrumentationFilter())

                .SetErrorStatusOnException()

                .SetSampler<AlwaysOnSampler>());

        Action<HttpClientTraceInstrumentationOptions> HttpClientInstrumentationFilter()
            => options => options.FilterHttpRequestMessage = request =>
            {
                if (Activity.Current?.Parent is null || request.RequestUri is null)
                    return false;

                var path = request.RequestUri.PathAndQuery.ToLowerInvariant();
                var excludedPaths = settings.PathsFilter.Concat(["/health/"]);

                if (!excludedPaths.Any(path.Contains))
                    return false;

                return settings.HttpRequestFilter is null || settings.HttpRequestFilter(request);
            };

        Action<AspNetCoreTraceInstrumentationOptions> AddAspNetCoreInstrumentationFilter()
            => options =>
            {
                options.RecordException = true;
                options.Filter = context =>
                {
                    if (Activity.Current?.Parent is null || context.Request is null || context.Request.Path.Value is null)
                        return false;

                    var path = context.Request.Path.Value!.ToLowerInvariant();

                    var excludedPaths = settings.PathsFilter.Concat(["/health/"]);

                    return !excludedPaths.Any(path.Contains);
                };
            };
    }

    public static OpenTelemetryBuilder ConfigureMetrics(this OpenTelemetryBuilder builder, TelemetrySettings settings)
    {
        return builder
            .WithMetrics(builder => builder

                .ConfigureResource(resource => resource
                    .AddService(
                        settings.ServiceName,
                        settings.ServiceNamespace,
                        settings.ServiceVersion,
                        settings.ServiceInstanceId is null,
                        settings.ServiceInstanceId)
                    .AddAttributes(settings.Attributes))

                .AddProcessInstrumentation()
                .AddRuntimeInstrumentation()

                .AddOtlpExporter((otlpOptions, metricsOptions) =>
                {
                    otlpOptions.Endpoint = new Uri($"{settings.CollectorEndpoint}/v1/metrics");
                    otlpOptions.Protocol = settings.CollectorProtocol.AsEnum<OtlpExportProtocol>(OtlpExportProtocol.Grpc);

                    metricsOptions.TemporalityPreference = settings.TemporalityPreference.AsEnum<MetricReaderTemporalityPreference>(MetricReaderTemporalityPreference.Delta);
                }));
    }
}

//public static class OpenTelemetryConfiguration
//{
//    public static void WithLogging(IServiceCollection services, OpenTelemetryBuilder builder, Action<LoggingOptions> optionsAction)
//    {
//        var options = new LoggingOptions();

//        optionsAction.Invoke(options);

//        builder.WithLogging(logging =>
//        {
//            logging.ConfigureResource(x => x
//                .AddService(
//                    options.ServiceName,
//                    options.ServiceNamespace,
//                    options.ServiceVersion,
//                    options.ServiceInstanceId is null,
//                    options.ServiceInstanceId)
//                .AddAttributes(options.Attributes));

//            logging.AddOtlpExporter(options =>
//            {

//            });
//        });

//        services.AddLogging(Configure);

//        void Configure(ILoggingBuilder loggingBuilder)
//        {
//            if (options.Filters.Count > 0)
//            {
//                options.Filters.ForEach(pair =>
//                    loggingBuilder.AddFilter(pair.Key, pair.Value));
//            }

//            loggingBuilder.AddOpenTelemetry(loggingOptions =>
//            {
//                loggingOptions.IncludeScopes = true;
//                loggingOptions.IncludeFormattedMessage = true;
//                loggingOptions.ParseStateValues = true;

//                var resource = ResourceBuilder
//                    .CreateDefault()
//                    .AddService(
//                        options.ServiceName,
//                        options.ServiceNamespace,
//                        options.ServiceVersion,
//                        options.ServiceInstanceId is null,
//                        options.ServiceInstanceId)
//                    .AddAttributes(options.Attributes);

//                loggingOptions.SetResourceBuilder(resource);
//            });
//        }
//    }

//    public sealed record LoggingOptions
//    {
//        public string ServiceName { get; set; } = $"Service_{Guid.NewGuid().AsString(7)}";
//        public string? ServiceNamespace { get; set; }
//        public string? ServiceVersion { get; set; }
//        public string? ServiceInstanceId { get; set; }
//        public List<KeyValuePair<string, object>> Attributes { get; set; } = [];
//        public List<KeyValuePair<string, LogLevel>> Filters { get; set; } = [];
//    }

//    public sealed record OtlpExporterOptions();

//    //public static OpenTelemetryBuilder ConfigureOpenTelemetry(this WebApplicationBuilder builder)
//    //{
//    //    builder.Logging.AddOpenTelemetry(o =>
//    //    {
//    //        o.IncludeFormattedMessage = true;
//    //        o.IncludeScopes = true;
//    //    });

//    //    return builder.Services.AddOpenTelemetry();
//    //}

//    //public static OpenTelemetryBuilder ConfigureLoki(this OpenTelemetryBuilder builder, OpenTelemetrySettings settings)
//    //{
//    //    builder
//    //        .Services
//    //        .ConfigureOpenTelemetryLoggerProvider(logging => logging.AddOtlpExporter(ExporterOptions()));

//    //    return builder;

//    //    Action<OtlpExporterOptions> ExporterOptions() => options =>
//    //    {
//    //        options.Endpoint = new Uri(settings.Loki.Endpoint);
//    //        options.Protocol = settings.Loki.Protocol.AsEnum<OtlpExportProtocol>(OtlpExportProtocol.HttpProtobuf);
//    //    };
//    //}

//    //public static IHostApplicationBuilder ConfigureOpenTelemetry(this IHostApplicationBuilder builder, AppIdSettings appId, OpenTelemetrySettings settings)
//    //{
//    //    builder.Logging.AddOpenTelemetry(options =>
//    //    {
//    //        options.IncludeScopes = true;
//    //        options.IncludeFormattedMessage = true;
//    //    });

//    //    builder.Services.AddOpenTelemetry()
//    //        .WithMetrics(metrics =>
//    //        {
//    //            metrics.AddAspNetCoreInstrumentation();
//    //            metrics.AddHttpClientInstrumentation();
//    //            metrics.AddProcessInstrumentation();
//    //            metrics.AddRuntimeInstrumentation()
//    //                .AddMeter(
//    //                    "Microsoft.AspNetCore.Hosting",
//    //                    "Microsoft.AspNetCore.Server.Kestrel",
//    //                    "System.Net.Http",
//    //                    "FwksLabs.ResumeApp.Web.Api");
//    //            metrics.AddPrometheusExporter();

//    //            metrics.ConfigureResource(ConfigureService(appId));
//    //        })
//    //        .WithTracing(tracing =>
//    //        {
//    //            if (builder.Environment.IsDevelopment())
//    //                tracing.SetSampler<AlwaysOnSampler>();

//    //            tracing
//    //                .AddAspNetCoreInstrumentation()
//    //                .AddHttpClientInstrumentation();

//    //            tracing.ConfigureResource(ConfigureService(appId));
//    //        });

//    //    builder.AddExporters(settings);

//    //    return builder;

//    //    static Action<ResourceBuilder> ConfigureService(AppIdSettings appId)
//    //        => resource => resource
//    //            .AddService(appId.Service.Kebaberize(), appId.GetFullName().Kebaberize(), appId.Version);
//    //}

//    //public static WebApplication UseOpenTelemetry(this WebApplication app)
//    //{
//    //    app
//    //        .UseOpenTelemetryPrometheusScrapingEndpoint();

//    //    return app;
//    //}

//    //private static void AddExporters(this IHostApplicationBuilder builder, OpenTelemetrySettings settings)
//    //{
//    //    builder.Services.ConfigureOpenTelemetryLoggerProvider(logging => logging.AddOtlpExporter(ExporterOptions()));
//    //    builder.Services.ConfigureOpenTelemetryMeterProvider(metrics => metrics.AddOtlpExporter(ExporterOptions()));
//    //    builder.Services.ConfigureOpenTelemetryTracerProvider(tracer => tracer.AddOtlpExporter(ExporterOptions()));

//    //    Action<OtlpExporterOptions> ExporterOptions() => options =>
//    //    {
//    //        options.Endpoint = new Uri(settings.Endpoint);
//    //        options.Protocol = settings.Protocol.AsEnum<OtlpExportProtocol>(OtlpExportProtocol.Grpc);
//    //    };
//    //}
//}