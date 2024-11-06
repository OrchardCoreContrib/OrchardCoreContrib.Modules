using OrchardCore.Navigation;

namespace OrchardCoreContrib.Navigation;

/// <summary>
/// Represents a contract for providing the navigation.
/// </summary>
public interface INavigationProvider : OrchardCore.Navigation.INavigationProvider
{
    /// <summary>
    /// Gets the menu name.
    /// </summary>
    string Name { get; }

    /// <summary>
    /// Builds the navigation menu.
    /// </summary>
    /// <param name="builder">The <see cref="NavigationBuilder"/>.</param>
    Task BuildNavigationAsync(NavigationBuilder builder);
}
