using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Infra.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace FwksLabs.Orbita.Infra.Postgres.Abstractions;

public interface IDatabaseContext : IInfra
{
    DbSet<ResumeEntity> Resumes { get; set; }
    DbSet<SkillEntity> Skills { get; set; }
    DbSet<EducationRecordEntity> Education { get; set; }
    DbSet<ExperienceRecordEntity> Experience { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
