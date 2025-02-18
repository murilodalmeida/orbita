using System;
using Humanizer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Overrides;

public sealed class CorsConfigurationOptionsOverride(
    ILogger<CorsConfigurationOptionsOverride> logger,
    string[] allowedOrigins,
    string[] allowedHeaders,
    string[] allowedMethods,
    string[] exposedHeaders) : IConfigureOptions<CorsOptions>
{
    private readonly ILogger<CorsConfigurationOptionsOverride> logger = logger;
    private readonly string[] allowedOrigins = allowedOrigins;
    private readonly string[] allowedHeaders = allowedHeaders;
    private readonly string[] allowedMethods = allowedMethods;
    private readonly string[] exposedHeaders = exposedHeaders;

    public void Configure(CorsOptions options)
    {
        logger.LogDebug("Configuring '{OptionsType}'", GetType().Name.Titleize());

        if (allowedOrigins.Length == 0)
            InvalidOperation("AllowedOrigins");

        if (allowedHeaders.Length == 0)
            InvalidOperation("AllowedHeaders");

        if (allowedMethods.Length == 0)
            InvalidOperation("AllowedMethods");

        options.AddDefaultPolicy(builder =>
        {
            builder
                .WithOrigins(allowedOrigins)
                .WithHeaders(allowedHeaders)
                .WithMethods(allowedMethods)
                .WithExposedHeaders(exposedHeaders)
                .AllowCredentials();
        });

        static void InvalidOperation(string argument) =>
           throw new InvalidOperationException($"{argument} is required.");
    }
}