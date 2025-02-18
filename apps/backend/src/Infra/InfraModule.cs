using System;
using FwksLabs.Orbita.Core.Configuration.Settings;
using FwksLabs.Orbita.Infra.LiteDb;
using FwksLabs.Orbita.Infra.Mongo;
using FwksLabs.Orbita.Infra.Postgres;
using Microsoft.Extensions.Caching.StackExchangeRedis;
using Microsoft.Extensions.DependencyInjection;
using ZiggyCreatures.Caching.Fusion;
using ZiggyCreatures.Caching.Fusion.Serialization.SystemTextJson;

namespace FwksLabs.Orbita.Infra;

public static class InfraModule
{
    public static IServiceCollection AddInfraModule(this IServiceCollection services, AppSettings appSettings) =>
        services
            .AddRedisCache(appSettings)
            .AddPostgresModule(appSettings)
            .AddMongoModule(appSettings)
            .AddLiteDbModule(appSettings);

    private static IServiceCollection AddRedisCache(this IServiceCollection services, AppSettings appSettings)
    {
        services
            .AddFusionCache()
            .WithDefaultEntryOptions(x =>
            {
                x.Duration = TimeSpan.FromMinutes(5);
            })
            .WithSerializer(new FusionCacheSystemTextJsonSerializer())
            .AsHybridCache()
            .WithDistributedCache(
                new RedisCache(
                    new RedisCacheOptions
                    {
                        Configuration = appSettings.Redis.ConnectionString
                    }
                )
            );

        return services;
    }
}
