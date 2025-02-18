using System;
using FwksLabs.Libs.Infra.Postgres.Abstractions;
using FwksLabs.Libs.Infra.Postgres.Configuration;
using FwksLabs.Libs.Infra.Postgres.Extensions;
using FwksLabs.Orbita.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FwksLabs.Orbita.Infra.Postgres.Configuration;

public sealed class ResumeEntityConfiguration : BaseEntityConfiguration<Guid, ResumeEntity>, ITypeConfiguration
{
    public override void Extend(EntityTypeBuilder<ResumeEntity> builder)
    {
        builder
            .HasIndex(x => x.Handle)
            .IsUnique();

        builder
            .OwnsOne(x => x.Name).ToJson();

        builder
            .OwnsOne(x => x.ContactInformation).ToJson();

        builder
            .OwnsOne(x => x.Location).ToJson();

        builder
            .OwnsMany(x => x.Socials).ToJson();

        builder
            .HasMany(x => x.Skills)
            .WithOne(x => x.Resume)
            .HasForeignKey(x => x.ResumeId, TableName!);

        builder
            .HasMany(x => x.Education)
            .WithOne(x => x.Resume)
            .HasForeignKey(x => x.ResumeId, TableName!);

        builder
            .HasMany(x => x.Experience)
            .WithOne(x => x.Resume)
            .HasForeignKey(x => x.ResumeId, TableName!);
    }
}
