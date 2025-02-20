namespace FwksLabs.Orbita.Core.ValueObjects;

public sealed record NameValue(string First, string Last, string? Alternate = default);

//{
//    public required string First { get; set; }
//    public required string Last { get; set; }
//    public string? Alternate { get; set; }
//}