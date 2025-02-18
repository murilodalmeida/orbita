using FluentValidation;

namespace FwksLabs.Orbita.Core.ValueObjects.Validators;

public sealed class LocationValueValidator : AbstractValidator<LocationValue>
{
    public LocationValueValidator()
    {
        RuleFor(x => x.City).NotEmpty();
        RuleFor(x => x.State).NotEmpty();
        RuleFor(x => x.Country).NotEmpty();
    }
}