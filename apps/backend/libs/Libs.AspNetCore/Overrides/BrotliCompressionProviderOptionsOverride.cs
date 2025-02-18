using System.IO.Compression;
using Humanizer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Overrides;
public class BrotliCompressionProviderOptionsOverride(
    ILogger<BrotliCompressionProviderOptionsOverride> logger) : IConfigureOptions<BrotliCompressionProviderOptions>
{
    private readonly ILogger<BrotliCompressionProviderOptionsOverride> logger = logger;

    public virtual void Configure(BrotliCompressionProviderOptions options)
    {
        logger.LogDebug("Configuring '{OptionsType}'", GetType().Name.Titleize());

        options.Level = CompressionLevel.Optimal;
    }
}