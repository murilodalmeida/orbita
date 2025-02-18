using System;
using Humanizer;
using Microsoft.Extensions.Primitives;

namespace FwksLabs.Libs.Core.Extensions;

public static class StringExtension
{
    public static bool EqualsTo(this string source, string target) => string.Equals(source, target, StringComparison.InvariantCultureIgnoreCase);

    public static bool EqualsTo(this StringValues source, string target) => string.Equals(source.ToString(), target, StringComparison.InvariantCultureIgnoreCase);

    public static bool HasValue(this string? source) => source is not null && !string.IsNullOrWhiteSpace(source);

    public static string PluralizeEntity(this string name) => name[..^"Entity".Length].Pluralize();

    public static string UpdateIfNotEmpty(this string source, string fallback = "") => string.IsNullOrWhiteSpace(source) ? fallback : source;
}