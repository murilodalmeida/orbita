using System;
using FwksLabs.Libs.Core.Types;
using FwksLabs.Orbita.Core.Enums;
using FwksLabs.Orbita.Core.ValueObjects;

namespace FwksLabs.Orbita.Core.Entities;

public sealed class EducationRecordEntity : BaseEntity<Guid>
{
    public Guid ResumeId { get; set; }
    public required EducationCategory Category { get; set; }
    public required string Organization { get; set; }
    public required string Degree { get; set; }
    public required string FieldOfStudy { get; set; }
    public TimeSpanValue? Period { get; set; }
    public LocationValue? Location { get; set; }

    public ResumeEntity? Resume { get; set; }
}