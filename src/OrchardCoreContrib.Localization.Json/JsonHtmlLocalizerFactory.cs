using Microsoft.AspNetCore.Mvc.Localization;
using Microsoft.Extensions.Localization;
using System;

namespace OrchardCoreContrib.Localization.Json
{
    /// <summary>
    /// Represents an <see cref="IHtmlLocalizerFactory"/> for JSON.
    /// </summary>
    public class JsonHtmlLocalizerFactory : IHtmlLocalizerFactory
    {
        private readonly IStringLocalizerFactory _stringLocalizerFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonHtmlLocalizerFactory"/> class.
        /// </summary>
        /// <param name="stringLocalizerFactory">The <see cref="IStringLocalizerFactory"/>.</param>
        public JsonHtmlLocalizerFactory(IStringLocalizerFactory stringLocalizerFactory)
        {
            _stringLocalizerFactory = stringLocalizerFactory;
        }

        /// <inheritdocs />
        public IHtmlLocalizer Create(string baseName, string location)
            => new JsonHtmlLocalizer(_stringLocalizerFactory.Create(baseName, location));

        /// <inheritdocs />
        public IHtmlLocalizer Create(Type resourceSource)
            => new JsonHtmlLocalizer(_stringLocalizerFactory.Create(resourceSource));
    }
}
