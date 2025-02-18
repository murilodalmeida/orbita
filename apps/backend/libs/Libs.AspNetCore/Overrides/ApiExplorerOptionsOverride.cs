using Asp.Versioning.ApiExplorer;
using Humanizer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Overrides;

public class ApiExplorerOptionsOverride(
    ILogger<ApiExplorerOptionsOverride> logger) : IConfigureOptions<ApiExplorerOptions>
{
    private readonly ILogger<ApiExplorerOptionsOverride> logger = logger;

    public virtual void Configure(ApiExplorerOptions options)
    {
        logger.LogDebug("Configuring '{OptionsType}'", GetType().Name.Titleize());

        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
}