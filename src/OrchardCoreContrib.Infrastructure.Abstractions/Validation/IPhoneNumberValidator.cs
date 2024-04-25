namespace OrchardCoreContrib.Validation;

/// <summary>
/// Represents a contract for a phone number validation.
/// </summary>
public interface IPhoneNumberValidator
{
    /// <summary>
    /// Gets whether a given phone number is valid or not.
    /// </summary>
    /// <param name="phoneNumber">The phone number to be validated.</param>
    bool IsValid(string phoneNumber);
}
