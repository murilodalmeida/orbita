using System.Collections.Generic;
using FwksLabs.Libs.Core.HealthCheck.Models;
using FwksLabs.Libs.Core.Settings;
using FwksLabs.Orbita.Core.Configuration.Settings.Properties;

namespace FwksLabs.Orbita.Core.Configuration.Settings;

public sealed record AppSettings(
    AuthServerSettings AuthServer,
    CorsSettings Cors,
    ConnectionStringSettings LiteDb,
    LocalizationSettings Localization,
    ConnectionStringSettings Mongo,
    ConnectionStringSettings Postgres,
    ConnectionStringSettings Redis,
    IDictionary<string, HttpServiceDependencySettings> Services,
    TelemetrySettings Telemetry
)
{
    public bool IsDevelopment { get; set; }
}

//"Logging": {
//            "Endpoint": "http://localhost:3100",
//            "Labels": {},
//            "PropertyLabels": [],
//            "MinimumLevel": "Debug",
//            "MinimumLevelOverride": {
//                "Microsoft.AspNetCore.Hosting": "Debug",
//                "Microsoft.AspNetCore.Mvc": "Debug",
//                "Microsoft.AspNetCore.Routing": "Debug",
//                "Microsoft.EntityFramework": "Debug"
//            }
//        }

//AppIdSettings AppId,
//OpenTelemetrySettings OpenTelemetry,
//SerilogSettings Logger,

//public record SerilogSettings(
//    string MinimumLevel,
//    IDictionary<string, string> MinimumLevelOverride);

//Dictionary<string, string> Labels,
//IEnumerable<string> PropertyLabels

//"AppId": {
//    "Platform": "core",
//    "Domain": "core",
//    "Service": "orbita-service",
//    "Version": "1.0.0-dev",
//    "Team": "BAGP!"
//},
//public sealed record AppIdSettings(string Platform, string Domain, string Service, string Version, string Team)
//{
//    public string GetFullName() => $"{Domain}.{Platform}.{Service}";
//}

//public record OpenTelemetrySettings(LokiSettings Loki);

//public record LokiSettings
//{
//    public required string Endpoint { get; set; }
//    public Dictionary<string, string> Labels { get; set; } = [];
//    public IEnumerable<string> PropertyLabels { get; set; } = [];
//}

//public sealed record LoggingOptions
//{
//    public required string ServiceName { get; set; }
//    public string? ServiceNamespace { get; set; }
//    public string? ServiceVersion { get; set; }
//    public string? ServiceInstanceId { get; set; }
//    public List<KeyValuePair<string, object>> Attributes { get; set; } = [];
//    public List<KeyValuePair<string, LogLevel>> Filters { get; set; } = [];
//}

//public sealed record OtlpExporterOptions
//{
//    public required string Endpoint { get; set; }
//    public ExportProcessorType ExportProcessorType { get; set; }
//    public OtlpExportProtocol Protocol { get; set; }

//    public Uri GetEndpointUri() => new($"{Endpoint}/otlp");
//    }

//    public static void WithLogging(this IServiceCollection services, OpenTelemetryBuilder builder, LoggingOptions loggingOptions, OtlpExporterOptions exporterOptions)
//    {
//        builder.WithLogging(logging =>
//        {
//            logging.ConfigureResource(x => x.FromOptions(loggingOptions));

//            logging.AddOtlpExporter(otlp =>
//            {
//                otlp.Endpoint = exporterOptions.GetEndpointUri();
//                otlp.Protocol = OtlpExportProtocol.HttpProtobuf;
//            });
//        });

//        services.AddLogging(Configure);

//        void Configure(ILoggingBuilder loggingBuilder)
//        {
//            if (loggingOptions.Filters.Count > 0)
//            {
//                loggingOptions.Filters.ForEach(pair =>
//                    loggingBuilder.AddFilter(pair.Key, pair.Value));
//            }

//            loggingBuilder.AddOpenTelemetry(otelOptions =>
//            {
//                otelOptions.IncludeScopes = true;
//                otelOptions.IncludeFormattedMessage = true;
//                otelOptions.ParseStateValues = true;

//                otelOptions.SetResourceBuilder(
//                    ResourceBuilder.CreateDefault().FromOptions(loggingOptions));

//                otelOptions.AddOtlpExporter(otlp =>
//                {
//                    otlp.Endpoint = exporterOptions.GetEndpointUri();
//                    otlp.ExportProcessorType = exporterOptions.ExportProcessorType;
//                    otlp.Protocol = exporterOptions.Protocol;

//                });
//            });
//        }
//    }

//    private static ResourceBuilder FromOptions(this ResourceBuilder builder, LoggingOptions options)
//    {
//        return builder
//                .AddService(
//                    options.ServiceName,
//                    options.ServiceNamespace,
//                    options.ServiceVersion,
//                    options.ServiceInstanceId is null,
//                    options.ServiceInstanceId)
//                .AddAttributes(options.Attributes);
//    }