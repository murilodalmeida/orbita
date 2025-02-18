using FwksLabs.Libs.Core.Configuration;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Overrides;
public sealed class JsonOptionsOverride : IConfigureOptions<JsonOptions>
{
    public void Configure(JsonOptions options)
    {
        var @default = JsonSerializerOptionsConfiguration.Options;

        options.SerializerOptions.PropertyNameCaseInsensitive = @default.PropertyNameCaseInsensitive;
        options.SerializerOptions.PropertyNamingPolicy = @default.PropertyNamingPolicy;
        options.SerializerOptions.WriteIndented = @default.WriteIndented;
        options.SerializerOptions.ReferenceHandler = @default.ReferenceHandler;
        options.SerializerOptions.AllowTrailingCommas = @default.AllowTrailingCommas;

        options.SerializerOptions.Converters.Clear();

        foreach (var converter in @default.Converters)
            options.SerializerOptions.Converters.Add(converter);
    }
}