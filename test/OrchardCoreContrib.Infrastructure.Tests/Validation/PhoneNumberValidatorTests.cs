using Xunit;

namespace OrchardCoreContrib.Validation.Tests;

public class PhoneNumberValidatorTests
{
    [InlineData("1-718-444-1122", true)]
    [InlineData("718-444-1122", true)]
    [InlineData("(718)-444-1122", true)]
    [InlineData("17184441122", true)]
    [InlineData("7184441122", true)]
    [InlineData("718.444.1122", true)]
    [InlineData("1718.444.1122", true)]
    [InlineData("1-123-456-7890", true)]
    [InlineData("1 123-456-7890", true)]
    [InlineData("1 (123) 456-7890", true)]
    [InlineData("1 123 456 7890", true)]
    [InlineData("1.123.456.7890", true)]
    [InlineData("+91 (123) 456-7890", true)]
    [InlineData("18005551234", true)]
    [InlineData("1 800 555 1234", true)]
    [InlineData("+1 800 555-1234", true)]
    [InlineData("+86 800 555 1234", true)]
    [InlineData("1-800-555-1234", true)]
    [InlineData("1 (800) 555-1234", true)]
    [InlineData("(800)555-1234", true)]
    [InlineData("(800) 555-1234", true)]
    [InlineData("(800)5551234", true)]
    [InlineData("800-555-1234", true)]
    [InlineData("800.555.1234", true)]
    [InlineData("18001234567", true)]
    [InlineData("1 800 123 4567", true)]
    [InlineData("1-800-123-4567", true)]
    [InlineData("+18001234567", true)]
    [InlineData("+1 800 123 4567", true)]
    [InlineData("+1 (800) 123 4567", true)]
    [InlineData("1(800)1234567", true)]
    [InlineData("+1800 1234567", true)]
    [InlineData("1.8001234567", true)]
    [InlineData("1.800.123.4567", true)]
    [InlineData("+1 (800) 123-4567", true)]
    [InlineData("+1 800 123-4567", true)]
    [InlineData("+86 800 123 4567", true)]
    [InlineData("1 (800) 123-4567", true)]
    [InlineData("(800)123-4567", true)]
    [InlineData("(800) 123-4567", true)]
    [InlineData("(800)1234567", true)]
    [InlineData("800-123-4567", true)]
    [InlineData("800.123.4567", true)]
    [InlineData("1231231231", true)]
    [InlineData("123-1231231", true)]
    [InlineData("123123-1231", true)]
    [InlineData("123-123 1231", true)]
    [InlineData("123 123-1231", true)]
    [InlineData("123-123-1231", true)]
    [InlineData("(123)123-1231", true)]
    [InlineData("(123)123 1231", true)]
    [InlineData("(123) 123-1231", true)]
    [InlineData("(123) 123 1231", true)]
    [InlineData("+99 1234567890", true)]
    [InlineData("+991234567890", true)]
    [InlineData("(555) 444-6789", true)]
    [InlineData("555-444-6789", true)]
    [InlineData("555.444.6789", true)]
    [InlineData("555 444 6789", true)]
    [InlineData("1.800.555.1234", true)]
    [InlineData("+1.800.555.1234", true)]
    [InlineData("(003) 555-1212", true)]
    [InlineData("(103) 555-1212", true)]
    [InlineData("(911) 555-1212", true)]
    [Theory]
    public void ValidatePhoneNumber(string phoneNumber, bool isValid)
    {
        // Arrange
        var phoneValidator = new PhoneNumberValidator();

        // Act
        var result = phoneValidator.IsValid(phoneNumber);

        // Assert
        Assert.Equal(isValid, result);
    }
}
