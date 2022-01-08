using OrchardCoreContrib.Localization.Diacritics;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// Represents an extension methods for <see cref="IServiceCollection"/>.
    /// </summary>
    public static class LocalizationServiceCollectionExtensions
    {
        /// <summary>
        /// Adds required services for diacritics into DI.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static IServiceCollection AddDiacritics(this IServiceCollection services)
        {
            if (services is null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IDiacriticsLookup, DiacriticsLookup>();

            return services;
        }
    }
}
