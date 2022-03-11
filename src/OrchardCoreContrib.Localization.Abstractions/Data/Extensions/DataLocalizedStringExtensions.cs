using OrchardCoreContrib.Infrastructure;
using System.Collections.Generic;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Provides an extension methods for <see cref="DataLocalizedString"/>.
    /// </summary>
    public static class DataLocalizedStringExtensions
    {
        /// <summary>
        /// Gets the string resource with the given name and context.
        /// </summary>
        /// <param name="dataLocalizer">The <see cref="IDataLocalizer"/>.</param>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="name">The context of the string resource.</param>
        public static DataLocalizedString GetString(this IDataLocalizer dataLocalizer, string name, string context)
        {
            Guard.ArgumentNotNull(nameof(dataLocalizer), dataLocalizer);
            Guard.ArgumentNotNullOrEmpty(nameof(name), name);
            Guard.ArgumentNotNullOrEmpty(nameof(context), context);

            return dataLocalizer[name, context];
        }

        /// <summary>
        /// Gets the string resource with the given name, context and formatted with the supplied arguments.
        /// </summary>
        /// <param name="dataLocalizer">The <see cref="IDataLocalizer"/>.</param>
        /// <param name="name">The name of the string resource.</param>
        /// <param name="context">The context of the string resource.</param>
        /// <param name="arguments">The values to format the string with.</param>
        public static DataLocalizedString GetString(this IDataLocalizer dataLocalizer, string name, string context, params object[] arguments)
        {
            Guard.ArgumentNotNull(nameof(dataLocalizer), dataLocalizer);
            Guard.ArgumentNotNullOrEmpty(nameof(name), name);
            Guard.ArgumentNotNullOrEmpty(nameof(context), context);

            return dataLocalizer[name, context, arguments];
        }

        /// <summary>
        /// Gets all string resources including those for parent cultures with a given context.
        /// </summary>
        /// <param name="dataLocalizer">The <see cref="IDataLocalizer"/>.</param>
        /// <param name="context">The context of the data resource.</param>
        /// <returns>The string resources.</returns>
        public static IEnumerable<DataLocalizedString> GetAllStrings(this IDataLocalizer dataLocalizer, string context)
        {
            Guard.ArgumentNotNull(nameof(dataLocalizer), dataLocalizer);
            Guard.ArgumentNotNullOrEmpty(nameof(context), context);

            return dataLocalizer.GetAllStrings(context, includeParentCultures: true);
        }
    }
}
