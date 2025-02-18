using FwksLabs.Orbita.Core.Configuration.Settings;
using FwksLabs.Orbita.Infra.Postgres.Abstractions;
using FwksLabs.Orbita.Infra.Postgres.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Orbita.Infra.Postgres;

public static class PostgresModule
{
    public static IServiceCollection AddPostgresModule(this IServiceCollection services, AppSettings appSettings) =>
        services
            .AddDbContext<IDatabaseContext, DatabaseContext>(x => x.UseNpgsql(appSettings.Postgres.ConnectionString));
}
