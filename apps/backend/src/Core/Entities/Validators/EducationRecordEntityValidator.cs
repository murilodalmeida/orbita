using FluentValidation;
using FwksLabs.Orbita.Core.ValueObjects;

namespace FwksLabs.Orbita.Core.Entities.Validators;

public sealed class EducationRecordEntityValidator : AbstractValidator<EducationRecordEntity>
{
    public EducationRecordEntityValidator(
        IValidator<TimeSpanValue> timeSpanValidator,
        IValidator<LocationValue> locationValidator)
    {
        RuleFor(x => x.Organization).NotEmpty();
        RuleFor(x => x.Period).SetValidator(timeSpanValidator!).When(x => x is not null);
        RuleFor(x => x.Location).SetValidator(locationValidator!).When(x => x is not null);
    }
}