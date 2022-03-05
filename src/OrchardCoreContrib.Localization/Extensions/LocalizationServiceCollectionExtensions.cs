using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.Localization.Diacritics;

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
            Guard.ArgumentNotNull(nameof(services), services);

            services.AddSingleton<IDiacriticsLookup, DiacriticsLookup>();

            return services;
        }
    }
}
