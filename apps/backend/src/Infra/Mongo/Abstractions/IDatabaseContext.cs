using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Infra.Abstractions;
using MongoDB.Driver;

namespace FwksLabs.Orbita.Infra.Mongo.Abstractions;

public interface IDatabaseContext : IInfra
{
    public IMongoCollection<ResumeEntity> Resumes { get; }
    public IMongoCollection<SkillEntity> Skills { get; }
    public IMongoCollection<EducationRecordEntity> Education { get; }
    public IMongoCollection<ExperienceRecordEntity> Experience { get; }
}
