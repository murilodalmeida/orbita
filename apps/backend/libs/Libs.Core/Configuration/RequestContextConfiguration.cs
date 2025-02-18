using FwksLabs.Libs.Core.Abstractions.Contexts;
using FwksLabs.Libs.Core.Contexts;
using Microsoft.Extensions.DependencyInjection;

namespace FwksLabs.Libs.Core.Configuration;

public static class RequestContextConfiguration
{
    public static IServiceCollection AddRequestContext(this IServiceCollection services) =>
        services
            .AddSingleton<IRequestContext, RequestContext>();
}