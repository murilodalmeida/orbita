namespace FwksLabs.Orbita.Core.ValueObjects;

public sealed record SocialMediaValue
{
    public required string Url { get; set; }
    public required string Label { get; set; }
}