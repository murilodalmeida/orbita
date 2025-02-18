namespace FwksLabs.Libs.Core.Settings;

public record ConnectionStringSettings(string ConnectionString)
{
    public string? Database { get; set; }
}