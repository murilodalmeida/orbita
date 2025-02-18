using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Constants;
using FwksLabs.Libs.Core.Abstractions.Contexts;
using FwksLabs.Libs.Core.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FwksLabs.Libs.AspNetCore.Services;
public sealed class ExceptionHandlerService(
    ILogger<ExceptionHandlerService> logger,
    IRequestContext requestContext) : IExceptionHandlerService
{
    private readonly ILogger<ExceptionHandlerService> _logger = logger;
    private readonly IRequestContext requestContext = requestContext;

    public async Task HandleAsync(HttpContext context, Exception? exception)
    {
        _logger.LogError(exception, "An unexpected error has occurred.");

        context.Response.ContentType = MediaTypeNames.Application.ProblemJson;
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        _ = context.Response.Headers.TryAdd(AppHeaders.CorrelationId, requestContext.CorrelationId);

        await context.Response.WriteAsJsonAsync(Problems.InternalServerError());
    }
}