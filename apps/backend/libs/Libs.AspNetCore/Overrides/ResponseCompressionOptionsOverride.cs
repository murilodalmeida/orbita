using System.Linq;
using Humanizer;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Overrides;

public class ResponseCompressionOptionsOverride(
    ILogger<ResponseCompressionOptionsOverride> logger) : IConfigureOptions<ResponseCompressionOptions>
{
    private readonly ILogger<ResponseCompressionOptionsOverride> logger = logger;

    public virtual void Configure(ResponseCompressionOptions options)
    {
        logger.LogDebug("Configuring '{OptionsType}'", GetType().Name.Titleize());

        options.EnableForHttps = true;
        options.Providers.Add<GzipCompressionProvider>();
        options.Providers.Add<BrotliCompressionProvider>();
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(["application/json", "application/problem+json"]);
    }
}