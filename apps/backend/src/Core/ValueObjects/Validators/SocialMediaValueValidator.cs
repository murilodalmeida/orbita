using FluentValidation;

namespace FwksLabs.Orbita.Core.ValueObjects.Validators;

public sealed class SocialMediaValueValidator : AbstractValidator<SocialMediaValue>
{
    public SocialMediaValueValidator()
    {
        RuleFor(x => x.Url).NotEmpty();
        RuleFor(x => x.Label).NotEmpty();
    }
}