using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Infra.Mongo.Abstractions;
using FwksLabs.Orbita.Core.Configuration.Settings;
using FwksLabs.Orbita.Infra.Mongo.Abstractions;
using FwksLabs.Orbita.Infra.Mongo.Contexts;
using Humanizer;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace FwksLabs.Orbita.Infra.Mongo;

public static class MongoModule
{
    public static IServiceCollection AddMongoModule(this IServiceCollection services, AppSettings appSettings) =>
        services
            .AddEntityConfiguration()
            .AddScoped<IMongoClient>(_ => new MongoClient(appSettings.Mongo.ConnectionString))
            .AddScoped<IDatabaseContext>(sp => new DatabaseContext(sp.GetRequiredService<IMongoClient>().GetDatabase(appSettings.Mongo.Database.Camelize())));

    private static IServiceCollection AddEntityConfiguration(this IServiceCollection services)
    {
        _ = typeof(InfraModule).FindConfigurationFromAssembly<ITypeConfiguration>();

        BsonSerializer.RegisterSerializer(new GuidSerializer(GuidRepresentation.Standard));

        return services;
    }
}