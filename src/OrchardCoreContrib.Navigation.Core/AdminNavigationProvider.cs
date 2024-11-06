namespace OrchardCoreContrib.Navigation;

/// <summary>
/// Represents a base class for an admin menu.
/// </summary>
public abstract class AdminNavigationProvider : NavigationProvider
{
    /// <inheritdoc/>
    public override string Name => "Admin";
}
