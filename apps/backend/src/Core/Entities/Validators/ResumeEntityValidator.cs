using FluentValidation;
using FwksLabs.Orbita.Core.ValueObjects;

namespace FwksLabs.Orbita.Core.Entities.Validators;
public sealed class ResumeEntityValidator : AbstractValidator<ResumeEntity>
{
    public ResumeEntityValidator(
        IValidator<NameValue> nameValidator,
        IValidator<ContactValue> contactInfoValidator,
        IValidator<LocationValue> locationValidator,
        IValidator<SkillEntity> skillValidator,
        IValidator<EducationRecordEntity> educationValidator,
        IValidator<ExperienceRecordEntity> experienceValidator)
    {
        RuleFor(x => x.Handle).NotEmpty();

        RuleFor(x => x.Name).SetValidator(nameValidator);

        RuleFor(x => x.JobTitle).NotEmpty();

        RuleFor(x => x.ContactInformation).SetValidator(contactInfoValidator);

        RuleFor(x => x.Location).SetValidator(locationValidator);

        RuleFor(x => x.Skills).ForEach(x => x.SetValidator(skillValidator));

        RuleFor(x => x.Education).ForEach(x => x.SetValidator(educationValidator));

        RuleFor(x => x.Experience)
            .Must(x => x.Count > 0).WithMessage("At least one experience record is required")
            .ForEach(x => x.SetValidator(experienceValidator));
    }
}