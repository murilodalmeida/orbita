using FluentAssertions;
using FwksLabs.Libs.Core.Extensions;
using Humanizer;

namespace FwksLabs.Libs.Core.Tests.Extensions;

[Trait("Type", "Unit")]
[Trait("Category", "Extensions")]
public sealed class StringExtensionTests
{
    [Theory("IsEqualTo", "when source and target are equal should return true")]
    [MemberData(nameof(IsEqualToTheoryData.SourceAndTargetAreEqual), MemberType = typeof(IsEqualToTheoryData))]
    public void IsEqualToReturnsTrue(string source, string target)
    {
        source.EqualsTo(target).Should().BeTrue();
    }

    [Theory("IsEqualTo", "when source and target are equal should return true")]
    [MemberData(nameof(IsEqualToTheoryData.SourceAndTargetAreDifferent), MemberType = typeof(IsEqualToTheoryData))]
    public void IsEqualToReturnsFalse(string source, string target)
    {
        source.EqualsTo(target).Should().BeFalse();
    }
}

public sealed class IsEqualToTheoryData
{
    public static TheoryData<string, string> SourceAndTargetAreEqual()
    {
        return new()
        {
            { "value", "VALUE" },
            { "Value", "value" },
            { "VALUE", "value" },
            { "valuE", "value" },
        };
    }

    public static TheoryData<string, string> SourceAndTargetAreDifferent()
    {
        return new()
        {
            { "value", "not-equal" },
            { "value", string.Empty },
            { "value", "different-value" },
        };
    }
}

public sealed class FactAttribute : Xunit.FactAttribute
{
    public FactAttribute(string method, string condition) =>
        DisplayName = $"\"{method}\", {condition.Humanize()}";
}

public sealed class TheoryAttribute : Xunit.TheoryAttribute
{
    public TheoryAttribute(string method, string condition) =>
        DisplayName = $"\"{method}\", {condition.Humanize()}";
}