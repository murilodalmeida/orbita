using System.IO.Compression;
using Humanizer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Overrides;

public class GzipCompressionProviderOptionsOverride(
    ILogger<GzipCompressionProviderOptionsOverride> logger) : IConfigureOptions<GzipCompressionProviderOptions>
{
    private readonly ILogger<GzipCompressionProviderOptionsOverride> logger = logger;

    public virtual void Configure(GzipCompressionProviderOptions options)
    {
        logger.LogDebug("Configuring '{OptionsType}'", GetType().Name.Titleize());

        options.Level = CompressionLevel.Optimal;
    }
}