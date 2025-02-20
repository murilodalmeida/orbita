using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Orbita.Core.Logs;
using FwksLabs.Orbita.Infra.LiteDb.Abstractions;
using FwksLabs.Orbita.Web.Api.Endpoints.Resume.Requests;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Endpoints;

internal sealed class UpdateHandleEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder.MapPatch("{id}/handle", HandleAsync)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);

    private static async Task<IResult> HandleAsync(
        [FromRoute] Guid id,
        [FromBody] UpdateHandleRequest request,
        [FromServices] IValidator<UpdateHandleRequest> validator,
        [FromServices] IDatabaseContext context,
        [FromServices] ILogger<UpdateHandleEndpoint> logger,
        [FromServices] ActivitySource activitySource,
        CancellationToken cancellationToken)
    {
        using var activity = activitySource.StartActivity("Patch Resume Handle", ActivityKind.Internal);

        var result = await validator.ValidateAsync(request, cancellationToken);
        if (result.IsValid is false)
        {
            logger.InvalidHandle(request.Handle);
            return AppResponses.ValidationErrors(result);
        }

        var resume = context.Resumes.FindById(id);
        if (resume is null)
        {
            logger.InvalidHandle(request.Handle);
            return AppResponses.NotFound();
        }

        resume.UpdateHandle(request.Handle);

        context.Resumes.Update(resume);

        return AppResponses.NoContent();
    }
}