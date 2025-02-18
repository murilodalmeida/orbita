using System;
using FwksLabs.Libs.Infra.Postgres.Abstractions;
using FwksLabs.Libs.Infra.Postgres.Configuration;
using FwksLabs.Orbita.Core.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Orbita.Infra.Postgres.Configuration;

public sealed class SkillEntityConfiguration : BaseEntityConfiguration<Guid, SkillEntity>, ITypeConfiguration
{
    public override void Extend(EntityTypeBuilder<SkillEntity> builder)
    {
        builder
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}