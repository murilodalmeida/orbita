using System;

namespace FwksLabs.Orbita.Core.ValueObjects;

public sealed record TimeSpanValue(DateOnly Start, DateOnly? End);

//{
//    public required DateOnly Started { get; set; }
//    public DateOnly? Ended { get; set; }
//}