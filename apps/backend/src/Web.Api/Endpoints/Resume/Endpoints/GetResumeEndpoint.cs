using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Orbita.Core.Constants;
using FwksLabs.Orbita.Core.Entities;
using FwksLabs.Orbita.Web.Api.Endpoints.Resume.Responses;
using FwksLabs.Orbita.Web.Api.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using ZiggyCreatures.Caching.Fusion;
using ILiteDbContext = FwksLabs.Orbita.Infra.LiteDb.Abstractions.IDatabaseContext;
using IMongoContext = FwksLabs.Orbita.Infra.Mongo.Abstractions.IDatabaseContext;
using IPostgresContext = FwksLabs.Orbita.Infra.Postgres.Abstractions.IDatabaseContext;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume.Endpoints;

public sealed class GetResumeEndpoint : IEndpoint
{
    public void Map(IEndpointRouteBuilder builder) => builder
        .MapGet("{handle}", HandleAsync)
        .Produces<ResumeResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status404NotFound);

    private static async Task<IResult> HandleAsync(
        [FromRoute] string handle,
        [FromServices] IPostgresContext pContext,
        [FromServices] ILiteDbContext lContext,
        [FromServices] IMongoContext mContext,
        [FromServices] IFusionCache cache,
        HttpContext context,
        CancellationToken cancellationToken)
    {
        var key = AppCacheKeys.Resume_GetByHandle(handle);

        var entity = context.Request.GetDbType() switch
        {
            "mongo" => await GetAsync(mContext.Resumes.Find(Query())
                .FirstOrDefaultAsync(cancellationToken)!),

            "psql" => await GetAsync(pContext.Resumes.Include(x => x.Skills).Include(x => x.Education).Include(x => x.Experience)
                .FirstOrDefaultAsync(Query(), cancellationToken)),

            _ => lContext.Resumes.FindOne(Query()),
        };

        if (entity is null)
            return AppResponses.NotFound();

        return AppResponses.Ok(ResumeResponse.FromEntity(entity));

        async ValueTask<ResumeEntity?> GetAsync(Task<ResumeEntity?> task) =>
            await cache.GetOrSetAsync(key.Name, async _ => await task, token: cancellationToken);

        Expression<Func<ResumeEntity, bool>> Query() => x => x.Handle.ToLower().Equals(handle.ToLower());
    }
}