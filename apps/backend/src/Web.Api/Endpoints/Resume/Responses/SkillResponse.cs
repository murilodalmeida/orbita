using System;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Core.Enums;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Responses;
public record SkillResponse(
    Guid Id,
    SkillCategory Category,
    string Name,
    int Level
)
{
    public static SkillResponse FromEntity(SkillEntity entity)
        => new(entity.Id, entity.Category, entity.Name, entity.Level);
}