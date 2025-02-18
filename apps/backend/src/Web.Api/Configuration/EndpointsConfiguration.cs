using System.Globalization;
using Asp.Versioning;
using FluentValidation;
using FwksLabs.Orbita.Core.Abstractions;
using FwksLabs.Orbita.Core.Configuration.Settings;
using FwksLabs.Orbita.Web.Api.Endpoints.Resume;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Orbita.Web.Api.Configuration;

internal static class EndpointsConfiguration
{
    internal static IServiceCollection AddEndpointsValidators(this IServiceCollection services, AppSettings appSettings)
    {
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo(appSettings.Localization.DefaultCulture);

        return services
            .AddValidatorsFromAssembly(typeof(ICore).Assembly)
            .AddValidatorsFromAssembly(typeof(Program).Assembly);
    }

    internal static IEndpointRouteBuilder MapEndpoints(this WebApplication app)
    {
        var versionSet = app.NewApiVersionSet()
            .HasApiVersion(new ApiVersion(1))
            .HasApiVersion(new ApiVersion(2))
            .ReportApiVersions()
            .Build();

        app
            .MapResumeEndpoints(versionSet);

        return app;
    }
}