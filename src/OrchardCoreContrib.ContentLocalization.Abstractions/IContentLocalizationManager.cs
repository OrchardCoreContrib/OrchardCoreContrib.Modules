using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrchardCoreContrib.ContentLocalization
{
    /// <summary>
    /// Represents a contract for managing the localization content.
    /// </summary>
    public interface IContentLocalizationManager : OrchardCore.ContentLocalization.IContentLocalizationManager
    {
        /// <summary>
        /// Gets the list of the localization set.
        /// </summary>
        Task<IEnumerable<string>> GetSetsAsync();
    }
}
