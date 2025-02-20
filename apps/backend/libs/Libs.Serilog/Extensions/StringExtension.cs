using Serilog.Sinks.Grafana.Loki;

namespace FwksLabs.Libs.Serilog.Extensions;

public static class StringExtension
{
    public static LokiLabel ToLokiLabel(this string key, string value)
        => new() { Key = key, Value = value };
}