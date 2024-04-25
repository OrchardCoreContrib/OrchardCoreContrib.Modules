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
            new("Article", "Content Type"),
            new("Article", "Menu"),
            new("First Name", "Content Field"),
            new("Last Name", "Content Field")
        };

        return Task.FromResult(recordsKeys.AsEnumerable());
    }
}
