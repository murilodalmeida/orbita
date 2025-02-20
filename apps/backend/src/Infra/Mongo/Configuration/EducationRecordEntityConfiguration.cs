using FwksLabs.Libs.Infra.Mongo.Abstractions;
using FwksLabs.Orbita.Core.Entities;
using MongoDB.Bson.Serialization;

namespace FwksLabs.Orbita.Infra.Mongo.Configuration;

public sealed class EducationRecordEntityConfiguration : ITypeConfiguration
{
    public EducationRecordEntityConfiguration()
    {
        BsonClassMap.RegisterClassMap<EducationRecordEntity>(mapper =>
        {
            mapper.AutoMap();
            mapper.UnmapProperty(x => x.ResumeId);
            mapper.UnmapProperty(x => x.Resume);
        });
    }
}