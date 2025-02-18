using FwksLabs.Libs.AspNetCore.Overrides;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace FwksLabs.Libs.AspNetCore.Configuration;

public static class CompressionOverrideConfiguration
{
    public static IServiceCollection AddCompressionOverride(this IServiceCollection services)
        => services
            .AddTransient<IConfigureOptions<GzipCompressionProviderOptions>, GzipCompressionProviderOptionsOverride>()
            .AddTransient<IConfigureOptions<BrotliCompressionProviderOptions>, BrotliCompressionProviderOptionsOverride>()
            .AddTransient<IConfigureOptions<ResponseCompressionOptions>, ResponseCompressionOptionsOverride>();

    public static IServiceCollection AddCompressionOverride<TGzipOverride, TBrotliOverride, TResponseOverride>(this IServiceCollection services)
        where TGzipOverride : class, IConfigureOptions<GzipCompressionProviderOptions>
        where TBrotliOverride : class, IConfigureOptions<BrotliCompressionProviderOptions>
        where TResponseOverride : class, IConfigureOptions<ResponseCompressionOptions>
            => services
                .AddTransient<IConfigureOptions<GzipCompressionProviderOptions>, TGzipOverride>()
                .AddTransient<IConfigureOptions<BrotliCompressionProviderOptions>, TBrotliOverride>()
                .AddTransient<IConfigureOptions<ResponseCompressionOptions>, TResponseOverride>();
}