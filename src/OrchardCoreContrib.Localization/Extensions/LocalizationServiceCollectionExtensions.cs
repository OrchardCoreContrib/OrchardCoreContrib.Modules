using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.Localization.Data;
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
        /// Registers the services to enable localization using data storage.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static IServiceCollection AddDataLocalization(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IDataTranslationProvider, NullDataTranslationProvider>();
            services.AddTransient<DataResourceManager>();
            services.AddSingleton<IDataLocalizerFactory, DataLocalizerFactory>();
            
            services.AddTransient(sp => {
                var dataLocalizerFactory = sp.GetService<IDataLocalizerFactory>();

                return dataLocalizerFactory.Create();
            });

            return services;
        }

        /// <summary>
        /// Adds required services for diacritics into DI.
        /// </summary>
        /// <param name="services">The <see cref="IServiceCollection"/>.</param>
        public static IServiceCollection AddDiacritics(this IServiceCollection services)
        {
            Guard.ArgumentNotNull(services, nameof(services));

            services.AddSingleton<IDiacriticsLookup, DiacriticsLookup>();

            return services;
        }
    }
}
