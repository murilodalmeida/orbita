using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Web.Api.Endpoints.Resume.Requests;
using FwksLabs.Orbita.Web.Api.Endpoints.Resume.Responses;
using FwksLabs.Orbita.Web.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using ILiteDbContext = FwksLabs.Orbita.Infra.LiteDb.Abstractions.IDatabaseContext;
using IMongoContext = FwksLabs.Orbita.Infra.Mongo.Abstractions.IDatabaseContext;
using IPostgresContext = FwksLabs.Orbita.Infra.Postgres.Abstractions.IDatabaseContext;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Endpoints;

public sealed class CreateResumeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapPost(string.Empty, HandleAsync)
        .Produces<CreateResumeResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest);

    private static async Task<IResult> HandleAsync(
        [FromBody] CreateResumeRequest request,
        [FromServices] IPostgresContext pContext,
        [FromServices] ILiteDbContext lContext,
        [FromServices] IMongoContext mContext,
        [FromServices] IValidator<CreateResumeRequest> requestValidator,
        [FromServices] IValidator<ResumeEntity> entityValidator,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        var result = await requestValidator.ValidateAsync(request, cancellationToken);
        if (!result.IsValid)
            return AppResponses.ValidationErrors(result);

        var entity = request.ToEntity();
        if (entity is null)
            return AppResponses.UnprocessableEntity("Invalid data. Chek the input values and try again");

        result = await entityValidator.ValidateAsync(entity, cancellationToken);
        if (!result.IsValid)
            return AppResponses.ValidationErrors(result);

        switch (context.Request.GetDbType())
        {
            case "mongo":
                await mContext.Resumes.InsertOneAsync(entity, null, cancellationToken);
                break;

            case "psql":
                await pContext.Resumes.AddAsync(entity, cancellationToken);
                await pContext.SaveChangesAsync(cancellationToken);
                break;

            default:
                lContext.Resumes.Insert(entity);
                break;
        }

        return AppResponses.Created(new CreateResumeResponse(entity.Id, entity.Handle));
    }
}