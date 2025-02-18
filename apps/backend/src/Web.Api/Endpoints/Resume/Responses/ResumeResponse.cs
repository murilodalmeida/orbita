using System;
using System.Collections.Generic;
using System.Linq;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Core.ValueObjects;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Responses;
public record ResumeResponse(
    Guid Id,
    string Handle,
    NameValue Name,
    string JobTitle,
    string? Summary,
    ContactValue ContactInformation,
    LocationValue Location,
    IReadOnlyCollection<SocialMediaValue> Socials,
    IReadOnlyCollection<SkillResponse> Skills,
    IReadOnlyCollection<EducationRecordResponse> Education,
    IReadOnlyCollection<ExperienceRecordResponse> Experience
)
{
    public static ResumeResponse FromEntity(ResumeEntity entity) => new(
        entity.Id,
        entity.Handle,
        entity.Name,
        entity.JobTitle,
        entity.Summary,
        entity.ContactInformation,
        entity.Location,
        [.. entity.Socials],
        [.. entity.Skills.Select(SkillResponse.FromEntity)],
        [.. entity.Education.Select(EducationRecordResponse.FromEntity)],
        [.. entity.Experience.Select(ExperienceRecordResponse.FromEntity)]
    );
}