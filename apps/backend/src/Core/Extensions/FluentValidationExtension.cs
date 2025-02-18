using FluentValidation;
using FwksLabs.ResumeApp.Core.Entities;

namespace FwksLabs.Orbita.Core.Extensions;

public static class FluentValidationExtension
{
    public static IRuleBuilder<T, string> PhoneNumber<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder
        .Must(phone =>
        {
            var value = AppRegexPatterns.AcceptPhoneNumbersOnly().Replace(phone, string.Empty);

            return AppRegexPatterns.PhoneNumber().IsMatch(value);
        })
        .WithMessage("'{PropertyName}' must be a valid Phone Number");
}