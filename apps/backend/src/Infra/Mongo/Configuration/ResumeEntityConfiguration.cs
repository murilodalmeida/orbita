using FwksLabs.Libs.Infra.Mongo.Abstractions;
using FwksLabs.Orbita.Core.Entities;
using MongoDB.Bson.Serialization;

namespace FwksLabs.Orbita.Infra.Mongo.Configuration;

public sealed class ResumeEntityConfiguration : ITypeConfiguration
{
    public ResumeEntityConfiguration()
    {
        BsonClassMap.RegisterClassMap<ResumeEntity>(mapper =>
        {
            mapper.AutoMap();
        });
    }
}
