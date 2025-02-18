using System;
using System.Security.Cryptography;
using System.Text;

namespace FwksLabs.Libs.Core.Extensions;

public static class ObjectExtension
{
    public static string ToMd5(this object value)
    {
        return value switch
        {
            string str => Hash(str),
            _ => HandleObject(value)
        };

        static string Hash(string? stringValue)
        {
            if (stringValue is null || !stringValue.HasValue())
                return string.Empty;

            var bytes = Encoding.UTF8.GetBytes(stringValue);

            var hash = MD5.HashData(bytes);

            return Convert.ToHexString(hash);
        }

        static string HandleObject(object value)
        {
            var serialized = value?.Serialize();

            return serialized.HasValue() ? Hash(serialized) : string.Empty;
        }
    }
}