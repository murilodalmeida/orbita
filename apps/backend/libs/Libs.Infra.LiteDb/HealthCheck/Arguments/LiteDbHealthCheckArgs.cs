using LiteDB;

namespace FwksLabs.Libs.Infra.LiteDb.HealthCheck.Arguments;

public sealed record LiteDbHealthCheckArgs(ILiteDatabase LiteDatabase);