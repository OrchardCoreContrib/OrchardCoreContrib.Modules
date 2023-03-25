namespace OrchardCoreContrib.Navigation;

public interface INavigationProvider
{
    string MenuName { get; }

    Task BuildNavigationAsync(NavigationBuilder builder);
}
