using OrchardCore.Navigation;

namespace OrchardCoreContrib.Navigation;

public interface INavigationProvider : OrchardCore.Navigation.INavigationProvider
{
    string Name { get; }

    Task BuildNavigationAsync(NavigationBuilder builder);
}
