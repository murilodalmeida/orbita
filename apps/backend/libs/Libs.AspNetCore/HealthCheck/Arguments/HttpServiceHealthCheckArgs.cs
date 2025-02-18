using System.Net.Http;

namespace FwksLabs.Libs.AspNetCore.HealthCheck.Arguments;

public sealed record HttpServiceHealthCheckArgs(IHttpClientFactory HttpClientFactory, string Name, string ServiceUrl, string? HealthCheckPath);