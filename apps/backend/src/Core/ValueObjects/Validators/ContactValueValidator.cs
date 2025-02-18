using FluentValidation;
using FwksLabs.Orbita.Core.Extensions;

namespace FwksLabs.Orbita.Core.ValueObjects.Validators;

public sealed class ContactValueValidator : AbstractValidator<ContactValue>
{
    public ContactValueValidator()
    {
        RuleFor(x => x.Email).EmailAddress();
        RuleFor(x => x.PhoneNumber).PhoneNumber();
    }
}