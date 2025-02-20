using Microsoft.Extensions.Logging;

namespace FwksLabs.Orbita.Core.Logs;

public static partial class ResumeLogger
{
    [LoggerMessage(LogLevel.Error, "Failed to update the handle `{handle}`.")]
    public static partial void InvalidHandle(this ILogger logger, string handle);
}