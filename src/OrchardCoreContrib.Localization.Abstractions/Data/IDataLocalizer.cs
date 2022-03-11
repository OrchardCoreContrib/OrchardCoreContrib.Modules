using System.Collections.Generic;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Represents a contract for localizing dynamic data.
    /// </summary>
    public interface IDataLocalizer
    {
        /// <summary>
        /// Gets the string resource with the given name and context.
        /// </summary>
        /// <param name="name">The name of the data resource.</param>
        /// <param name="context">The context of the data resource.</param>
        DataLocalizedString this[string name, string context] { get; }

        /// <summary>
        /// Gets the string resource with the given name, context and formatted with the supplied rguments.
        /// </summary>
        /// <param name="name">The name of the data resource.</param>
        /// <param name="context">The context of the data resource.</param>
        /// <param name="arguments">The values to format the string with.</param>
        DataLocalizedString this[string name, string context, params object[] arguments] { get; }

        /// <summary>
        /// Gets all string resources.
        /// </summary>
        /// <param name="context">The context of the data resource.</param>
        /// <param name="includeParentCultures">A <see cref="Boolean"/> indicating whether to include strings from parent cultures.</param>
        IEnumerable<DataLocalizedString> GetAllStrings(string context, bool includeParentCultures);
    }
}
