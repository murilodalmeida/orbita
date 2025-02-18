using System;
using FwksLabs.Libs.AspNetCore.Abstractions;
using FwksLabs.Libs.AspNetCore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class ExceptionHandlerServiceConfiguration
{
    public static IServiceCollection AddExceptionHandlerService(this IServiceCollection services)
        => services.AddSingleton<IExceptionHandlerService, ExceptionHandlerService>();

    public static IServiceCollection AddExceptionHandlerService<TService>(this IServiceCollection services)
        where TService : class, IExceptionHandlerService
        => services.AddSingleton<IExceptionHandlerService, TService>();

    public static IApplicationBuilder UseExceptionHandlerService(this IApplicationBuilder builder) =>
        builder
            .UseExceptionHandler(handler =>
            {
                handler.Run(async context =>
                {
                    var service = context.RequestServices.GetService<IExceptionHandlerService>()
                        ?? throw new InvalidOperationException($"{nameof(IExceptionHandlerService)} is not registered.");

                    var exception = context.Features.Get<IExceptionHandlerFeature>();

                    await service.HandleAsync(context, exception?.Error);
                });
            });
}