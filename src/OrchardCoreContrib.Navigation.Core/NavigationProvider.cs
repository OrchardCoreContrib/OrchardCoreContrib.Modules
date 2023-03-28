namespace OrchardCoreContrib.Navigation;

public abstract class NavigationProvider : OrchardCore.Navigation.INavigationProvider
{
    public abstract string Name { get; }

    public virtual void BuildNavigation(OrchardCore.Navigation.NavigationBuilder builder)
    {

    }

    public virtual Task BuildNavigationAsync(OrchardCore.Navigation.NavigationBuilder builder)
    {
        BuildNavigation(builder);

        return Task.CompletedTask;
    }

    public async Task BuildNavigationAsync(string name, OrchardCore.Navigation.NavigationBuilder builder)
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
