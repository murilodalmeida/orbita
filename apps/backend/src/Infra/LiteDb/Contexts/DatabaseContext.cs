using FwksLabs.Libs.Infra.LiteDb.Extensions;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Infra.LiteDb.Abstractions;
using LiteDB;

namespace FwksLabs.Orbita.Infra.LiteDb.Contexts;

public sealed class DatabaseContext(LiteDatabase database) : IDatabaseContext
{
    private readonly LiteDatabase database = database;

    private ILiteCollection<ResumeEntity>? resumes;
    private ILiteCollection<SkillEntity>? skills;
    private ILiteCollection<EducationRecordEntity>? education;
    private ILiteCollection<ExperienceRecordEntity>? experience;

    public ILiteCollection<ResumeEntity> Resumes => resumes ??= database.GetNamedCollection<ResumeEntity>();
    public ILiteCollection<SkillEntity> Skills => skills ??= database.GetNamedCollection<SkillEntity>();
    public ILiteCollection<EducationRecordEntity> Education => education ??= database.GetNamedCollection<EducationRecordEntity>();
    public ILiteCollection<ExperienceRecordEntity> Experience => experience ??= database.GetNamedCollection<ExperienceRecordEntity>();
}