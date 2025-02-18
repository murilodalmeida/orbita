using FwksLabs.Libs.AspNetCore.Overrides;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class JsonOptionsOverrideConfiguration
{
    public static IServiceCollection AddHttpJsonOptionsOverride(this IServiceCollection services)
        => services.AddTransient<IConfigureOptions<JsonOptions>, JsonOptionsOverride>();

    public static IServiceCollection AddHttpJsonOptionsOverride<TJsonOptionsOverride>(this IServiceCollection services)
        where TJsonOptionsOverride : class, IConfigureOptions<JsonOptions>
            => services.AddTransient<IConfigureOptions<JsonOptions>, TJsonOptionsOverride>();
}