using OrchardCoreContrib.Localization.Diacritics;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class LocalizationServiceCollectionExtensions
    {
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
