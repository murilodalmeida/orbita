using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Infra.Abstractions;
using FwksLabs.Orbita.Infra.Postgres.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Orbita.Infra.Postgres.Contexts;

public sealed class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options), IDatabaseContext
{
    public DbSet<ResumeEntity> Resumes { get; set; }
    public DbSet<SkillEntity> Skills { get; set; }
    public DbSet<EducationRecordEntity> Education { get; set; }
    public DbSet<ExperienceRecordEntity> Experience { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        var assembly = typeof(IInfra).Assembly;

        modelBuilder.ApplyConfigurationsFromAssembly(assembly);
    }
}