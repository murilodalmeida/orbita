using FluentValidation;

namespace FwksLabs.Orbita.Core.ValueObjects.Validators;

public sealed class TimeSpanValueValidator : AbstractValidator<TimeSpanValue>
{
    public TimeSpanValueValidator()
    {
        RuleFor(x => x.Start).NotEmpty();
        RuleFor(x => x.End).Must((value, end) => end >= value.Start).When(x => x is not null);
    }
}