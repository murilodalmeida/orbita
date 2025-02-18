using Asp.Versioning;
using Asp.Versioning.ApiExplorer;
using FwksLabs.Libs.AspNetCore.Overrides;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class VersioningOverrideConfiguration
{
    public static IServiceCollection AddApiVersioningOverride(this IServiceCollection services)
        => services
            .AddTransient<IConfigureOptions<ApiVersioningOptions>, ApiVersioningOptionsOverride>()
            .AddTransient<IConfigureOptions<ApiExplorerOptions>, ApiExplorerOptionsOverride>();

    public static IServiceCollection AddApiVersioningOverride<TVersioningOverride, TExplorerOverride>(this IServiceCollection services)
        where TVersioningOverride : class, IConfigureOptions<ApiVersioningOptions>
        where TExplorerOverride : class, IConfigureOptions<ApiExplorerOptions>
    => services
        .AddTransient<IConfigureOptions<ApiVersioningOptions>, TVersioningOverride>()
        .AddTransient<IConfigureOptions<ApiExplorerOptions>, TExplorerOverride>();
}
