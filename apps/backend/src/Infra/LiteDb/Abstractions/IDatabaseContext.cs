using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Infra.Abstractions;
using LiteDB;

namespace FwksLabs.Orbita.Infra.LiteDb.Abstractions;

public interface IDatabaseContext : IInfra
{
    public ILiteCollection<ResumeEntity> Resumes { get; }
    public ILiteCollection<SkillEntity> Skills { get; }
    public ILiteCollection<EducationRecordEntity> Education { get; }
    public ILiteCollection<ExperienceRecordEntity> Experience { get; }
}
