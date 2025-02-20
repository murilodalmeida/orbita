using System;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Core.Enums;
using FwksLabs.Orbita.Core.ValueObjects;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Responses;

public record ExperienceRecordResponse(
    Guid Id,
    string CompanyName,
    string JobTitle,
    string? Description,
    EmploymentCategory? Category,
    TimeSpanValue? Period,
    LocationValue? Location
)
{
    public static ExperienceRecordResponse FromEntity(ExperienceRecordEntity entity)
        => new(entity.Id, entity.CompanyName, entity.JobTitle, entity.Description, entity.Category, entity.Period, entity.Location);
}