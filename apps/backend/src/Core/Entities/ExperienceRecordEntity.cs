using System;
using FwksLabs.Libs.Core.Types;
using FwksLabs.Orbita.Core.Enums;
using FwksLabs.Orbita.Core.ValueObjects;

namespace FwksLabs.Orbita.Core.Entities;

public sealed class ExperienceRecordEntity : BaseEntity<Guid>
{
    public override Guid Id { get; protected set; } = Guid.NewGuid();
    public override Guid ReferenceId { get; protected set; } = Guid.NewGuid();

    public Guid ResumeId { get; set; }
    public required string CompanyName { get; set; }
    public required string JobTitle { get; set; }
    public string? Description { get; set; }
    public EmploymentCategory? Category { get; set; }
    public TimeSpanValue? Period { get; set; }
    public LocationValue? Location { get; set; }

    public ResumeEntity? Resume { get; set; }
}
