using System;
using FwksLabs.Libs.Core.Types;
using FwksLabs.Orbita.Core.Enums;

namespace FwksLabs.Orbita.Core.Entities;

public sealed class SkillEntity : BaseEntity<Guid>
{
    public override Guid Id { get; protected set; } = Guid.NewGuid();
    public override Guid ReferenceId { get; protected set; } = Guid.NewGuid();

    public Guid ResumeId { get; set; }
    public required SkillCategory Category { get; set; }
    public required string Name { get; set; }
    public int Level { get; set; }

    public ResumeEntity? Resume { get; set; }
}