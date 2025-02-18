using System;
using FwksLabs.Libs.Infra.Postgres.Abstractions;
using FwksLabs.Libs.Infra.Postgres.Configuration;
using FwksLabs.Orbita.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Orbita.Infra.Postgres.Configuration;

public sealed class ExperienceEntityConfiguration : BaseEntityConfiguration<Guid, ExperienceRecordEntity>, ITypeConfiguration
{
    public override void Extend(EntityTypeBuilder<ExperienceRecordEntity> builder)
    {
        builder
            .OwnsOne(x => x.Period).ToJson();

        builder
            .OwnsOne(x => x.Location).ToJson();
    }
}