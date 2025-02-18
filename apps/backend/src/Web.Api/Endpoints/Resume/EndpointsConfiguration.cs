using Asp.Versioning.Builder;
using FwksLabs.Libs.AspNetCore.Extensions;
using FwksLabs.Orbita.Web.Api.Constants;
using FwksLabs.Orbita.Web.Api.Endpoints.Resume.Endpoints;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Orbita.Web.Api.Endpoints.Resume;

internal static class EndpointsConfiguration
{
    internal static RouteGroupBuilder MapResumeEndpoints(this WebApplication app, ApiVersionSet versionSet)
    {
        return app
            .MapVersionedGroup(AppEndpoints.Resume, versionSet)
            .WithTags(AppEndpoints.Resume)
            .MapToApiVersion(1)
            .MapEndpoint<CreateResumeEndpoint>()
            .MapEndpoint<GetResumeEndpoint>()
            .MapEndpoint<UpdateHandleEndpoint>()
            .MapEndpoint<UpdateResumeOverviewEndpoint>();
    }
}