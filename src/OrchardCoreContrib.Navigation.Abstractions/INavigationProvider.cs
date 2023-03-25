namespace OrchardCoreContrib.Navigation;

public interface INavigationProvider
{
    Task BuildNavigationAsync(string name, NavigationBuilder builder);
}
