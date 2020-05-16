﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OrchardCore.Localization;

namespace OrchardCoreContrib.Localization.Json
{
    /// <summary>
    /// Represents a <see cref="IStringLocalizerFactory"/> for portable objects.
    /// </summary>
    public class JsonStringLocalizerFactory : IStringLocalizerFactory
    {
        private readonly ILocalizationManager _localizationManager;
        private readonly bool _fallBackToParentCulture;
        private readonly ILogger _logger;

        /// <summary>
        /// Creates a new instance of <see cref="PortableObjectStringLocalizerFactory"/>.
        /// </summary>
        /// <param name="localizationManager"></param>
        /// <param name="requestLocalizationOptions"></param>
        /// <param name="logger"></param>
        public JsonStringLocalizerFactory(
            ILocalizationManager localizationManager,
            IOptions<RequestLocalizationOptions> requestLocalizationOptions,
            ILogger<JsonStringLocalizerFactory> logger)
        {
            _localizationManager = localizationManager;
            _fallBackToParentCulture = requestLocalizationOptions.Value.FallBackToParentUICultures;
            _logger = logger;
        }

        /// <inheritedoc />
        public IStringLocalizer Create(Type resourceSource)
            => new JsonStringLocalizer(_localizationManager, _fallBackToParentCulture, _logger);

        /// <inheritedoc />
        public IStringLocalizer Create(string baseName, string location)
            => new JsonStringLocalizer(_localizationManager, _fallBackToParentCulture, _logger);
    }
}
