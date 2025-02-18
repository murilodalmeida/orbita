using Humanizer;

namespace FwksLabs.Orbita.Core.Constants;

public record CacheKey(string Name, params string[] Tags)
{
    public override string ToString() => Name;
}

public static class AppCacheKeys
{
    public static CacheKey Resume_GetByHandle(string handle) => new($"resumes:{handle}", nameof(Resume_GetByHandle).Kebaberize());
}
