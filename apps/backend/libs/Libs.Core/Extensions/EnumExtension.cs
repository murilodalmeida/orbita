using System;

namespace FwksLabs.Libs.Core.Extensions;

public static class EnumExtension
{
    public static T AsEnum<T>(this string value, T? @default = default) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value, true, out var result))
            return result;

        if (@default.HasValue)
            return @default.Value;

        throw new InvalidCastException($"'{value}' is not a valid '{typeof(T).Name}' enum value.");
    }
}