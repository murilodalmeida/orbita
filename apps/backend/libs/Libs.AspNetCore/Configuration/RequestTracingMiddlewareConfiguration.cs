using System;
using FwksLabs.Libs.AspNetCore.Middlewares;
using FwksLabs.Libs.Core.Abstractions.Contexts;
using FwksLabs.Libs.Core.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class RequestTracingMiddlewareConfiguration
{
    public static IServiceCollection AddRequestTracingMiddleware(this IServiceCollection services)
        => services.AddScoped<RequestTracingMiddleware>();

    public static IApplicationBuilder UseRequestTracingMiddleware(this IApplicationBuilder appBuilder)
    {
        var requestContext = appBuilder.ApplicationServices.GetService<IRequestContext>();

        return requestContext is null
            ? throw new InvalidOperationException($"{nameof(RequestContext)} is not registered.")
            : appBuilder.UseMiddleware<RequestTracingMiddleware>();
    }
}