using OrchardCoreContrib.Templating;
using OrchardCoreContrib.Templating.Razor.Services;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// Provides extension methods for registering the Razor template engine services in an <see cref="IServiceCollection"/>.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers the Razor template engine services in the specified <see cref="IServiceCollection"/>.
    /// </summary>
    /// <param name="services">The <see cref="IServiceCollection"/> to add the services to.</param>
    /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
    public static IServiceCollection AddRazorTemplating(this IServiceCollection services)
    {
        services.AddSingleton<ITemplateEngine, RazorTemplateEngine>();

        return services;
    }
}
