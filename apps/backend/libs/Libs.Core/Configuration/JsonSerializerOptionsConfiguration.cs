using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;

namespace FwksLabs.Libs.Core.Configuration;

public static class JsonSerializerOptionsConfiguration
{
    private static readonly Lock _lock = new();

    private static JsonSerializerOptions? _options;

    public static JsonSerializerOptions Options
    {
        get
        {
            if (_options is not null)
                return _options;

            lock (_lock)
            {
                if (_options is not null)
                    return _options;

                _options = new(JsonSerializerDefaults.Web);

                Configure(_options);

                return _options;
            }
        }
    }

    public static void Configure(JsonSerializerOptions options)
    {
        options.PropertyNameCaseInsensitive = true;
        options.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
        options.WriteIndented = false;
        options.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.Converters.Add(new JsonStringEnumConverter());
        options.AllowTrailingCommas = true;
    }
}