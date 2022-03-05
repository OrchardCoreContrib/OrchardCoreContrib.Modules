using OrchardCore.Localization;
using System.Collections.Generic;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Represents a contract for providing resources from the data store.
    /// </summary>
    public interface IDataResourceStringProvider
    {
        /// <summary>
        /// Gets the resource strings.
        /// </summary>
        IEnumerable<CultureDictionaryRecordKey> GetAllResourceStrings();
    }
}
