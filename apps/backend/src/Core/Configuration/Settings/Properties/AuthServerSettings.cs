namespace FwksLabs.Orbita.Core.Configuration.Settings.Properties;

public sealed record AuthServerSettings(string Authority, string ClientId, string[] Audiences);