using System;

namespace FwksLabs.Libs.Core.Extensions;

public static class GuidExtension
{
    public static string AsString(this Guid guid, int length) => guid.ToString()[0..length];
}
