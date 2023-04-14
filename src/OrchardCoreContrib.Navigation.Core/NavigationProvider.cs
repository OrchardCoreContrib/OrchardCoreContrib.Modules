using NavigationBuilder = OrchardCore.Navigation.NavigationBuilder;

namespace OrchardCoreContrib.Navigation;

public abstract class NavigationProvider : INavigationProvider
{
    public abstract string Name { get; }

    public virtual void BuildNavigation(NavigationBuilder builder)
    {

    }

    public virtual Task BuildNavigationAsync(NavigationBuilder builder)
    {
        BuildNavigation(builder);

        return Task.CompletedTask;
    }

    public async Task BuildNavigationAsync(string name, NavigationBuilder builder)
    {
        if (name.Equals(Name, StringComparison.OrdinalIgnoreCase))
        {
            await BuildNavigationAsync(builder);
        }
        else
        {
            await Task.CompletedTask;
        }      
    }
}
