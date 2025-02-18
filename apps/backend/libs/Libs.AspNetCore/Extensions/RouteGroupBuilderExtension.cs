using System.Diagnostics.CodeAnalysis;
using System.Linq;
using Asp.Versioning.Builder;
using FwksLabs.Libs.AspNetCore.Abstractions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace FwksLabs.Libs.AspNetCore.Extensions;

public static class RouteGroupBuilderExtension
{
    public static RouteGroupBuilder MapEndpoint<TEndpoint>(this RouteGroupBuilder builder) where TEndpoint : IEndpoint, new()
    {
        new TEndpoint().Map(builder);

        return builder;
    }

    public static RouteGroupBuilder MapVersionedGroup(this IEndpointRouteBuilder builder, [StringSyntax("Route")] string prefix, ApiVersionSet versionSet, params int[] problems)
    {
        problems = [.. problems, StatusCodes.Status422UnprocessableEntity, StatusCodes.Status500InternalServerError];

        return builder
            .MapGroup($"v{{version:apiVersion}}/{prefix}")
            .WithApiVersionSet(versionSet)
            .ProducesProblems(problems.Distinct().ToArray());
    }

    public static RouteGroupBuilder ProducesProblems(this RouteGroupBuilder builder, params int[] problems)
    {
        foreach (var problem in problems)
            builder.ProducesProblem(problem);

        return builder;
    }
}