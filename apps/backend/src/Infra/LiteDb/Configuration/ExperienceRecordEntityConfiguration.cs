using FwksLabs.Libs.Infra.LiteDb.Abstractions;
using FwksLabs.Libs.Infra.LiteDb.Configuration;
using FwksLabs.Orbita.Core.Entities;
using LiteDB;

namespace FwksLabs.Orbita.Infra.LiteDb.Configuration;
public sealed class ExperienceRecordEntityConfiguration : BaseEntityConfiguration<ExperienceRecordEntity>, ITypeConfiguration
{
    public override void Extend(EntityBuilder<ExperienceRecordEntity> mapper)
    {
        mapper
            .Ignore(x => x.Id)
            .Ignore(x => x.ResumeId)
            .Ignore(x => x.Resume);
    }
}