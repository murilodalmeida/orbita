using FwksLabs.Libs.AspNetCore.HealthCheck.Extensions;
using FwksLabs.Libs.Infra.LiteDb.HealthCheck.Extensions;
using FwksLabs.Libs.Infra.Mongo.HealthCheck.Extensions;
using FwksLabs.Libs.Infra.Postgres.HealthCheck.Extensions;
using FwksLabs.Orbita.Core.Configuration.Settings;
using Microsoft.Extensions.Hosting;

namespace FwksLabs.Orbita.Web.Api.Configuration;

public static class HealthCheckConfiguration
{
    public static IHostApplicationBuilder ConfigureHealthChecks(this IHostApplicationBuilder builder, AppSettings appSettings)
    {
        builder.Services
            .AddPostgresHealthCheck(appSettings.Postgres.ConnectionString, appSettings.Postgres.Database!)
            .AddMongoHealthCheck(appSettings.Mongo.ConnectionString)
            .AddLiteDbHealthCheck(appSettings.LiteDb.ConnectionString)
            .AddHttpServicesHealthCheck(appSettings.Services);

        return builder;
    }
}