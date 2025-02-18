using System;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Core.Enums;
using FwksLabs.Orbita.Core.ValueObjects;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Responses;
public record EducationRecordResponse(
    Guid Id,
    EducationCategory Category,
    string Organization,
    string Degree,
    string FieldOfStudy,
    TimeSpanValue? Period,
    LocationValue? Location
)
{
    public static EducationRecordResponse FromEntity(EducationRecordEntity entity)
        => new(entity.Id, entity.Category, entity.Organization, entity.Degree, entity.FieldOfStudy, entity.Period, entity.Location);
}