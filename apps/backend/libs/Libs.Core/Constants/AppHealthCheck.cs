namespace FwksLabs.Libs.Core.Constants;

public static class AppHealthCheck
{
    public const string EndpointsLiveness = "/health/live";
    public const string EndpointsReadiness = "/health/ready";

    public const int TimeoutInSeconds = 10;

    public const string TagsLiveness = "liveness";
    public const string TagsReadiness = "readiness";
    public const string TagsCritical = "critical";
    public const string TagsNonCritical = "non-critical";
    public const string TagsTypeDatabase = "type-database";
    public const string TagsTypeHttpService = "type-httpservice";
}