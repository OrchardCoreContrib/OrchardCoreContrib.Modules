using OrchardCoreContrib.Infrastructure;
using System.Text.RegularExpressions;

namespace OrchardCoreContrib.Validation;

/// <summary>
/// Represents a service for a phone number validation.
/// </summary>

public partial class PhoneNumberValidator : IPhoneNumberValidator
{
    /// <inheritdoc />
    public bool IsValid(string phoneNumber)
    {
        Guard.ArgumentNotNull(phoneNumber, nameof(phoneNumber));
        
        return PhoneNumberRegex().IsMatch(phoneNumber);
    }

    [GeneratedRegex(@"^(\+\d{1,2}\s?)?1?\-?\.?\s?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$")]
    private static partial Regex PhoneNumberRegex();
}
