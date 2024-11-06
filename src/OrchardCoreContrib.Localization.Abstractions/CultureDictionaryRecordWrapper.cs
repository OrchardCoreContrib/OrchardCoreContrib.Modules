using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization;

/// <summary>
/// Represents a wrapper for <see cref="CultureDictionary"/>.
/// </summary>
/// <inheritdocs />
public class CultureDictionaryRecordWrapper(string messageId, string context, string[] translations)
    : CultureDictionaryRecord(messageId, context, translations)
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CultureDictionaryRecordWrapper"/> class.
    /// </summary>
    /// <param name="messageId">The message Id.</param>
    /// <param name="translations">a list of translations.</param>
    public CultureDictionaryRecordWrapper(string messageId, params string[] translations)
        : this(messageId, null, translations)
    {
    }
}
