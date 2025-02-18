namespace FwksLabs.Orbita.Core.Configuration.Settings.Properties;

public sealed record CorsSettings(string[] AllowedOrigins, string[] AllowedHeaders, string[] AllowedMethods, string[] ExposedHeaders);