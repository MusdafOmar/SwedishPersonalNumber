using PersonalNumber.Core;
using Xunit;

namespace PersonalNumber.Tests;

public class PersonalNumberValidatorTests
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("   ")]
    [InlineData("abcdef")]
    [InlineData("123")]
    public void InvalidInputs_ReturnFalse(string? input)
    {
        Assert.False(PersonalNumberValidator.IsValid(input));
    }

    [Theory]
    // Wrong lengths after removing separators
    [InlineData("19900101-001")]      // 11 digits
    [InlineData("19900101-00177")]    // 13 digits
    [InlineData("900101-001")]        // 9 digits
    public void WrongLength_ReturnFalse(string input)
    {
        Assert.False(PersonalNumberValidator.IsValid(input));
    }

    [Theory]
    // Invalid month / day
    [InlineData("19901301-1234")] // month 13
    [InlineData("19900199-1234")] // day 99
    public void InvalidDateParts_ReturnFalse(string input)
    {
        Assert.False(PersonalNumberValidator.IsValid(input));
    }

    [Fact]
    public void Should_Accept_Separators_And_Spaces()
    {
        // Same number with formatting (still may be invalid if checksum fails)
        var input = "19900101-0017";
        var inputWithSpaces = "  19900101-0017  ";

        Assert.Equal(
            PersonalNumberValidator.IsValid(input),
            PersonalNumberValidator.IsValid(inputWithSpaces)
        );
    }

    [Theory]
    // If you have teacher-provided valid numbers, put them here!
    // These are commonly seen examples in demos/training materials.
    [InlineData("19900101-0017")]
    [InlineData("199001010017")]
    [InlineData("900101-0017")]
    [InlineData("9001010017")]
    public void KnownValidExamples_ReturnTrue(string input)
    {
        Assert.True(PersonalNumberValidator.IsValid(input));
    }

    [Theory]
    // Same date/serial but different control digit -> should fail Luhn often
    [InlineData("19900101-0018")]
    [InlineData("900101-0018")]
    public void WrongChecksum_ReturnFalse(string input)
    {
        Assert.False(PersonalNumberValidator.IsValid(input));
    }

    [Theory]
    // Coordination number: day + 60 (e.g. 61 => day 1)
    // NOTE: checksum still must be correct; use examples your teacher provides if needed.
    [InlineData("19900161-0010")]
    public void CoordinationNumber_Format_ShouldNotCrash(string input)
    {
        // We only assert it doesn't throw and returns a boolean.
        // Replace with Assert.True when you have a confirmed valid coordination-number example.
        _ = PersonalNumberValidator.IsValid(input);
        Assert.True(true);
    }
}
