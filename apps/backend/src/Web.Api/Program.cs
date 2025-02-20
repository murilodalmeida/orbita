using System;
using FwksLabs.Libs.AspNetCore.Configuration;
using FwksLabs.Libs.AspNetCore.HealthCheck.Configuration;
using FwksLabs.Libs.Core.Configuration;
using FwksLabs.Orbita.Core.Configuration.Settings;
using FwksLabs.Orbita.Infra;
using FwksLabs.Orbita.Web.Api.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Scalar.AspNetCore;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var appSettings = GetAppSettings();

    Log.Logger = SerilogConfiguration.CreateLogger(appSettings);

    builder
        .ConfigureHealthChecks(appSettings)
        .ConfigureTelemetry(appSettings)
        .Services
        .AddLogging()
        .AddSerilog()
        .AddSingleton(appSettings)
        .AddOpenApi()
        .AddCors()
        .AddHttpClient()
        .AddRequestContext()
        .AddResponseCompression()
        .AddHttpContextAccessor()
        .AddEndpointsApiExplorer()
        .AddExceptionHandlerService()
        .AddRequestTracingMiddleware()
        .AddEndpointsValidators(appSettings)
        .AddApiVersioning()
        .AddApiExplorer().Services

        // Overrides
        .AddApiVersioningOverride()
        .AddHttpJsonOptionsOverride()
        .AddCompressionOverride()

        // Modules
        .AddInfraModule(appSettings);

    var app = builder.Build();

    if (appSettings.IsDevelopment)
    {
        app.MapOpenApi();
        app.MapScalarApiReference();
    }

    app
        .UseRequestTracingMiddleware()
        .UseSerilogRequestLogging()
        .UseHttpsRedirection()
        .UseCors()
        .UseResponseCompression()
        .UseExceptionHandlerService()
        .UseHealthCheckEndpoints();

    app.MapEndpoints();

    Log.Logger.Information("App is starting up.");

    await app.RunAsync();

    AppSettings GetAppSettings()
    {
        try
        {
            var settings = builder.Configuration.Get<AppSettings>()!;

            settings.IsDevelopment = builder.Environment.IsDevelopment();

            return settings;
        }
        catch (Exception e)
        {
            throw new InvalidOperationException("AppSettings configuration is missing.", e);
        }
    }
}
catch (Exception e)
{
    Log.Logger.Fatal(e, "App shutdown unexpectedly.");
}
finally
{
    Log.Logger.Information("App is shutting down.");
}