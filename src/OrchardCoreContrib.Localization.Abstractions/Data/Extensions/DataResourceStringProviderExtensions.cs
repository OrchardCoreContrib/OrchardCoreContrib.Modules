using OrchardCore.Localization;
using OrchardCoreContrib.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Provides an extension methods for <see cref="IDataResourceStringProvider"/>.
    /// </summary>
    public static class DataResourceStringProviderExtensions
    {
        /// <summary>
        /// Gets the resource strings.
        /// </summary>
        /// <param name="resourceStringProvider">The <see cref="IDataResourceStringProvider"/>.</param>
        /// <param name="context">The resource context.</param>
        public static IEnumerable<CultureDictionaryRecordKey> GetAllResourceStrings(this IDataResourceStringProvider resourceStringProvider, string context)
        {
            Guard.ArgumentNotNull(resourceStringProvider, nameof(resourceStringProvider));
            Guard.ArgumentNotNullOrEmpty(context, nameof(context));

            return resourceStringProvider
                .GetAllResourceStrings()
                .Where(s => s.GetContext().Equals(context, StringComparison.OrdinalIgnoreCase));
        }
    }
}
