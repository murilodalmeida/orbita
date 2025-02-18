using FluentValidation;
using FluentValidation.Validators;
using FwksLabs.Orbita.Core.Extensions;
using FwksLabs.Orbita.Web.Api.Endpoints.Resume.Requests;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Validators;

public sealed class CreateResumeRequestValidator : AbstractValidator<CreateResumeRequest>
{
    public CreateResumeRequestValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty();

        RuleFor(x => x.LastName).NotEmpty();

        RuleFor(x => x.JobTitle).NotEmpty();

        RuleFor(x => x.CompanyName).NotEmpty();

        RuleFor(x => x.Email).EmailAddress(EmailValidationMode.AspNetCoreCompatible);

        RuleFor(x => x.PhoneNumber).PhoneNumber();
    }
}