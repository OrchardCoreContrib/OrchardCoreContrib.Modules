using Microsoft.Extensions.Localization;
using System.Collections.Generic;

namespace OrchardCoreContrib.Sms;

/// <summary>
/// Represents the result of sending an SMS.
/// </summary>
public class SmsResult
{
    /// <summary>
    /// Returns an <see cref="SmsResult"/>indicating a successful SMS operation.
    /// </summary>
    public static SmsResult Success { get; } = new SmsResult { Succeeded = true };

    /// <summary>
    /// An <see cref="IEnumerable{LocalizedString}"/> containing an errors that occurred during the SMS operation.
    /// </summary>
    public IEnumerable<LocalizedString> Errors { get; protected set; }

    /// <summary>
    /// Whether if the operation succeeded or not.
    /// </summary>
    public bool Succeeded { get; protected set; }

    /// <summary>
    /// Creates an <see cref="SmsResult"/> indicating a failed SMS operation, with a list of errors if applicable.
    /// </summary>
    /// <param name="errors">An optional array of <see cref="LocalizedString"/> which caused the operation to fail.</param>
    public static SmsResult Failed(params LocalizedString[] errors) => new SmsResult { Succeeded = false, Errors = errors };
}
