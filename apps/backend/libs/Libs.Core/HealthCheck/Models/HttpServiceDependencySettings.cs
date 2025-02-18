namespace FwksLabs.Libs.Core.HealthCheck.Models;

public sealed record HttpServiceDependencySettings
{
    public string? Label { get; set; }
    public required string Url { get; set; }
    public int Timeout { get; set; } = 10;
    public bool Critical { get; set; } = false;
    public string? HealthCheckPath { get; set; }
};