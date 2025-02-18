using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;

namespace FwksLabs.Orbita.Web.Api.Extensions;

public static class HttpRequestExtension
{
    public static string GetDbType(this HttpRequest request) =>
        request.Headers.TryGetValue("X-DbType", out StringValues dbType) ? dbType.ToString() : "psql";
}