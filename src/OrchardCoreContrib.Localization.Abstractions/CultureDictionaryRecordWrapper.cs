using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization
{
    public class CultureDictionaryRecordWrapper : CultureDictionaryRecord
    {
        public CultureDictionaryRecordWrapper(string messageId, params string[] translations)
            : this(messageId, null, translations)
        {
        }

        public CultureDictionaryRecordWrapper(string messageId, string context, string[] translations)
            : base(messageId, context, translations)
        {
        }
    }
}
