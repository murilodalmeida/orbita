using FluentValidation;
using FwksLabs.Orbita.Core.ValueObjects;

namespace FwksLabs.Orbita.Core.Entities.Validators;
public sealed class ExperienceRecordEntityValidator : AbstractValidator<ExperienceRecordEntity>
{
    public ExperienceRecordEntityValidator(
        IValidator<TimeSpanValue> timeSpanValidator,
        IValidator<LocationValue> locationValidator)
    {
        RuleFor(x => x.CompanyName).NotEmpty();
        RuleFor(x => x.JobTitle).NotEmpty();
        RuleFor(x => x.Period).SetValidator(timeSpanValidator!).When(x => x is not null);
        RuleFor(x => x.Location).SetValidator(locationValidator!).When(x => x is not null);
    }
}