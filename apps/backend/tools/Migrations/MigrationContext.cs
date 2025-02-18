using FwksLabs.Orbita.Infra.Postgres.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace FwksLabs.ResumeApp.Migrations;

public sealed class MigrationContext : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var connString = "Host=localhost;Port=5432;Username=local;Password=local;Database=ResumeAppService;";

        var builder = new DbContextOptionsBuilder<DatabaseContext>()
            .UseNpgsql(connString, x =>
            {
                x.MigrationsHistoryTable("Migrations", "History");
                x.MigrationsAssembly(typeof(MigrationContext).Assembly.FullName);
            });

        return new DatabaseContext(builder.Options);
    }
}