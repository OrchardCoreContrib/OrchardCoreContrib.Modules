using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization
{
    /// <summary>
    /// Provides an extension methods for <see cref="CultureDictionaryRecordKey"/>.
    /// </summary>
    public static class CultureDictionaryRecordKeyExtensions
    {
        private const char KeySeparator = '|';

        /// <summary>
        /// Gets the context associated with the key.
        /// </summary>
        /// <param name="cultureDictionaryRecordKey">The <see cref="CultureDictionaryRecordKey"/>.</param>
        public static string GetContext(this CultureDictionaryRecordKey cultureDictionaryRecordKey)
        {
            var value = cultureDictionaryRecordKey.ToString();
            
            return value[..value.IndexOf(KeySeparator)];
        }

        /// <summary>
        /// Gets the message id associated with the key.
        /// </summary>
        /// <param name="cultureDictionaryRecordKey">The <see cref="CultureDictionaryRecordKey"/>.</param>
        public static string GetMessageId(this CultureDictionaryRecordKey cultureDictionaryRecordKey)
        {
            var value = cultureDictionaryRecordKey.ToString();

            return value[(value.IndexOf(KeySeparator) + 1)..];
        }
    }
}
