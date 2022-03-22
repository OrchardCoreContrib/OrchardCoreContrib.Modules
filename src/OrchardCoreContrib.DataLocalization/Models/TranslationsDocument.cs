using OrchardCore.Data.Documents;
using System;
using System.Collections.Generic;

namespace OrchardCoreContrib.DataLocalization.Models
{
    /// <summary>
    /// Represents a documents that contains a list of <see cref="Translation"/>.
    /// </summary>
    public class TranslationsDocument : Document
    {
        /// <summary>
        /// Creates a new instance of <see cref="TranslationsDocument"/>.
        /// </summary>
        public TranslationsDocument()
        {
            Translations = new Dictionary<string, IEnumerable<Translation>>(StringComparer.OrdinalIgnoreCase);
        }
        
        /// <summary>
        /// Gets the list of the translations associated with a culture.
        /// </summary>
        public Dictionary<string, IEnumerable<Translation>> Translations { get; }
    }
}
