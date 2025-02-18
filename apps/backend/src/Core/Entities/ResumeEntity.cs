using System;
using System.Collections.Generic;
using FwksLabs.Libs.Core.Extensions;
using FwksLabs.Libs.Core.Types;
using FwksLabs.Orbita.Core.ValueObjects;
using Humanizer;

namespace FwksLabs.Orbita.Core.Entities;

public sealed class ResumeEntity : BaseEntity<Guid>
{
    public override Guid Id { get; protected set; } = Guid.NewGuid();
    public override Guid ReferenceId { get; protected set; } = Guid.NewGuid();

    public string Handle { get; private set; } = string.Empty;
    public required NameValue Name { get; set; }
    public required string JobTitle { get; set; }
    public string? Summary { get; set; }
    public required ContactValue ContactInformation { get; set; }
    public required LocationValue Location { get; set; }
    public ICollection<SocialMediaValue> Socials { get; set; } = [];

    public ICollection<SkillEntity> Skills { get; set; } = [];
    public ICollection<EducationRecordEntity> Education { get; set; } = [];
    public ICollection<ExperienceRecordEntity> Experience { get; set; } = [];

    public static ResumeEntity? FromRequest(
        string firstName,
        string lastName,
        string jobTitle,
        string companyName,
        string email,
        string phoneNumber,
        string city,
        string state,
        string country)
    {
        var ett = new ResumeEntity
        {
            Name = new NameValue(firstName, lastName),
            JobTitle = jobTitle,
            ContactInformation = new ContactValue(email, phoneNumber),
            Location = new LocationValue(city, state, country),
            Experience = [new()
            {
                CompanyName = companyName,
                JobTitle = jobTitle
            }]
        };

        ett.BuildInitialHandle();

        return ett;
    }

    public void BuildInitialHandle() =>
        Handle = $"{Name.First} {Name.Last} {Guid.NewGuid().AsString(7)}".Kebaberize();

    public void UpdateHandle(string handle) =>
        Handle = handle.Kebaberize();
}
