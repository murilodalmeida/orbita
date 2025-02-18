using FwksLabs.Libs.Infra.Mongo.Extensions;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Infra.Mongo.Abstractions;
using MongoDB.Driver;

namespace FwksLabs.Orbita.Infra.Mongo.Contexts;

public sealed class DatabaseContext(IMongoDatabase database) : IDatabaseContext
{
    private readonly IMongoDatabase database = database;

    private IMongoCollection<ResumeEntity>? resumes;
    private IMongoCollection<SkillEntity>? skills;
    private IMongoCollection<EducationRecordEntity>? education;
    private IMongoCollection<ExperienceRecordEntity>? experience;

    public IMongoCollection<ResumeEntity> Resumes => resumes ??= database.GetNamedCollection<ResumeEntity>();
    public IMongoCollection<SkillEntity> Skills => skills ??= database.GetNamedCollection<SkillEntity>();
    public IMongoCollection<EducationRecordEntity> Education => education ??= database.GetNamedCollection<EducationRecordEntity>();
    public IMongoCollection<ExperienceRecordEntity> Experience => experience ??= database.GetNamedCollection<ExperienceRecordEntity>();
}