using System;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Orbita.Infra.LiteDb.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Endpoints;

internal sealed class UpdateResumeOverviewEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder.MapPatch("{id}/overview", HandleAsync)
        .Produces(StatusCodes.Status204NoContent)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .ProducesProblem(StatusCodes.Status404NotFound);

    private static async Task<IResult> HandleAsync(
        [FromBody] Guid id,
        [FromServices] IDatabaseContext context,
        CancellationToken cancellationToken)
    {
        // var resume = await context.Resumes.FirstOrDefaultAsync(cancellationToken);

        await Task.Yield();

        return AppResponses.NoContent();
    }
}
