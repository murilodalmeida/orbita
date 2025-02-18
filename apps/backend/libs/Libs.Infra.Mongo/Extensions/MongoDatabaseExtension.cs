using FwksLabs.Libs.Core.Extensions;
using MongoDB.Driver;

namespace FwksLabs.Libs.Infra.Mongo.Extensions;

public static class MongoDatabaseExtension
{
    public static IMongoCollection<T> GetNamedCollection<T>(this IMongoDatabase database) =>
        database.GetCollection<T>(typeof(T).Name.PluralizeEntity());
}