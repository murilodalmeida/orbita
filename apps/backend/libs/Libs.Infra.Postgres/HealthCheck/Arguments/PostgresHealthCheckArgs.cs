using Npgsql;

namespace FwksLabs.Libs.Infra.Postgres.HealthCheck.Arguments;

public sealed record PostgresHealthCheckArgs(NpgsqlDataSource Datasource);