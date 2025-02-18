using System.Text.RegularExpressions;

namespace FwksLabs.ResumeApp.Core.Entities;

public static partial class AppRegexPatterns
{
    [GeneratedRegex(@"^\+([1-9]\d{0,2})\s?(\d{2,14})$")]
    public static partial Regex PhoneNumber();

    [GeneratedRegex(@"(?!^\+)[^\d]")]
    public static partial Regex AcceptPhoneNumbersOnly();
}