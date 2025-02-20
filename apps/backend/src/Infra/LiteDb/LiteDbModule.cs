using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.LiteDb.Abstractions;
using FwksLabs.Orbita.Core.Configuration.Settings;
using FwksLabs.Orbita.Infra.Abstractions;
using FwksLabs.Orbita.Infra.LiteDb.Abstractions;
using FwksLabs.Orbita.Infra.LiteDb.Contexts;
using LiteDB;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Orbita.Infra.LiteDb;

public static class LiteDbModule
{
    public static IServiceCollection AddLiteDbModule(this IServiceCollection services, AppSettings appSettings) =>
        services
            .AddEntityConfiguration()
            .AddSingleton(_ => new LiteDatabase(appSettings.LiteDb.ConnectionString))
            .AddSingleton<IDatabaseContext>(sp => new DatabaseContext(sp.GetRequiredService<LiteDatabase>()));

    private static IServiceCollection AddEntityConfiguration(this IServiceCollection services)
    {
        _ = typeof(IInfra).FindConfigurationFromAssembly<ITypeConfiguration>();

        return services;
    }
}