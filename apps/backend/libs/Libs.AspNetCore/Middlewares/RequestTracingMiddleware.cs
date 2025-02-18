using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FwksLabs.Libs.Core.Abstractions.Contexts;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Http;

namespace FwksLabs.Libs.AspNetCore.Middlewares;

public sealed class RequestTracingMiddleware(
    IRequestContext requestContext) : IMiddleware
{
    private readonly IRequestContext requestContext = requestContext;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var correlationId = context.Request.Headers[AppHeaders.CorrelationId].SingleOrDefault();

        correlationId ??= Guid.NewGuid().ToString();

        requestContext.Initialize(correlationId, context.TraceIdentifier);

        context.Response.OnStarting(() =>
        {
            _ = context.Response.Headers.TryAdd(AppHeaders.CorrelationId, correlationId);

            return Task.CompletedTask;
        });

        await next(context);
    }
}