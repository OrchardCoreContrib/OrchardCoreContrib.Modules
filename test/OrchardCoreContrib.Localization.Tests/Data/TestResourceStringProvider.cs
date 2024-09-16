using OrchardCore.Localization;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Localization.Data.Tests;

internal class TestResourceStringProvider : IDataResourceStringProvider
{
    public Task<IEnumerable<CultureDictionaryRecordKey>> GetAllResourceStringsAsync()
    {
        var recordsKeys = new List<CultureDictionaryRecordKey>
        {
            new() { MessageId = "Article", Context = "Content Type" },
            new() { MessageId = "Article", Context = "Menu" },
            new() { MessageId = "First Name", Context = "Content Field" },
            new() { MessageId = "Last Name", Context = "Content Field" }
        };

        return Task.FromResult(recordsKeys.AsEnumerable());
    }
}
