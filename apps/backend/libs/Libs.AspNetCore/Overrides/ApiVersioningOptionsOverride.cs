using Asp.Versioning;
using Humanizer;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Overrides;

public class ApiVersioningOptionsOverride(
    ILogger<ApiVersioningOptionsOverride> logger) : IConfigureOptions<ApiVersioningOptions>
{
    private readonly ILogger<ApiVersioningOptionsOverride> logger = logger;

    public virtual void Configure(ApiVersioningOptions options)
    {
        logger.LogDebug("Configuring '{OptionsType}'", GetType().Name.Titleize());

        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.ReportApiVersions = true;
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }
}
