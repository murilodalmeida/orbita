using FluentValidation;
using FwksLabs.Orbita.Infra.LiteDb.Abstractions;
using FwksLabs.Orbita.Web.Api.Endpoints.Resume.Requests;
using Humanizer;
using Microsoft.Extensions.Logging;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Validators;

public sealed class UpdateHandleRequestValidator : AbstractValidator<UpdateHandleRequest>
{
    public UpdateHandleRequestValidator(
        ILogger<UpdateHandleRequestValidator> logger,
        IDatabaseContext context)
    {
        RuleFor(x => x.Handle).NotEmpty();

        RuleFor(x => x.Handle)
            .Must(handle =>
            {
                var exists = context.Resumes.FindOne(x => x.Handle.Equals(handle.Kebaberize())) is null;

                if (!exists)
                {
                    logger.LogError("This resume doesn't exist.");
                }

                return exists;
            })
            .WithMessage("Handle already in use.");
    }
}