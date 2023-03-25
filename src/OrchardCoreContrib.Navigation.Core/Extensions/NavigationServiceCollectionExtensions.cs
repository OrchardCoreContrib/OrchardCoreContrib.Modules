using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace OrchardCoreContrib.Navigation.Extensions;

public static class NavigationServiceCollectionExtensions
{
    /// <summary>
    /// Adds tenant level services.
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddNavigation(this IServiceCollection services)
    {
        services.TryAddEnumerable(ServiceDescriptor.Scoped<INavigationManager, NavigationManager>());

        return services;
    }
}
