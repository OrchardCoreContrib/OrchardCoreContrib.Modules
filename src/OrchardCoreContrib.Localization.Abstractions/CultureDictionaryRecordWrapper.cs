using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization;

/// <summary>
/// Represents a wrapper for <see cref="CultureDictionary"/>.
/// </summary>
public class CultureDictionaryRecordWrapper : CultureDictionaryRecord
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

    /// <inheritdocs />
    public CultureDictionaryRecordWrapper(string messageId, string context, string[] translations)
        : base(messageId, context, translations)
    {
    }
}
