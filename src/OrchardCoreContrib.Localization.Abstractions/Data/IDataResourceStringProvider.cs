using OrchardCore.Localization;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardCoreContrib.Localization.Data;

/// <summary>
/// Represents a contract for providing resources from the data store.
/// </summary>
public interface IDataResourceStringProvider
{
    [Obsolete("This method has been deprecated and will be removed in the future releases, please use GetAllResourceStringsAsync() instead.")]
    IEnumerable<CultureDictionaryRecordKey> GetAllResourceStrings() => GetAllResourceStringsAsync().GetAwaiter().GetResult();

    /// <summary>
    /// Gets the resource strings.
    /// </summary>
    Task<IEnumerable<CultureDictionaryRecordKey>> GetAllResourceStringsAsync();
}
