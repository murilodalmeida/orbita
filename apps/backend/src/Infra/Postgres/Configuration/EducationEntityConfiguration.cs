using System;
using FwksLabs.Libs.Infra.Postgres.Abstractions;
using FwksLabs.Libs.Infra.Postgres.Configuration;
using FwksLabs.Orbita.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Orbita.Infra.Postgres.Configuration;

public sealed class EducationEntityConfiguration : BaseEntityConfiguration<Guid, EducationRecordEntity>, ITypeConfiguration
{
    public override void Extend(EntityTypeBuilder<EducationRecordEntity> builder)
    {
        builder
            .OwnsOne(x => x.Period).ToJson();

        builder
            .OwnsOne(x => x.Location).ToJson();
    }
}