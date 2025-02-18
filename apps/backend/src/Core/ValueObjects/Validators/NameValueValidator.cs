using FluentValidation;

namespace FwksLabs.Orbita.Core.ValueObjects.Validators;

public sealed class NameValueValidator : AbstractValidator<NameValue>
{
    public NameValueValidator()
    {
        RuleFor(x => x.First).NotEmpty();
        RuleFor(x => x.Last).NotEmpty();
    }
}