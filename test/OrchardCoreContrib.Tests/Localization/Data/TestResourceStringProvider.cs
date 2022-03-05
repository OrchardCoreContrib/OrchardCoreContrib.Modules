using OrchardCore.Localization;
using System.Collections.Generic;

namespace OrchardCoreContrib.Localization.Data.Tests
{
    internal class TestResourceStringProvider : IDataResourceStringProvider
    {
        public IEnumerable<CultureDictionaryRecordKey> GetAllResourceStrings()
        {
            yield return new CultureDictionaryRecordKey("Article", "Content Type");
            yield return new CultureDictionaryRecordKey("Article", "Menu");
            yield return new CultureDictionaryRecordKey("First Name", "Content Field");
            yield return new CultureDictionaryRecordKey("Last Name", "Content Field");
        }
    }
}
