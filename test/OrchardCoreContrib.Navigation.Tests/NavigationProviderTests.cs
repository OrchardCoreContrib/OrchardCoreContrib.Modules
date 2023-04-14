using Microsoft.Extensions.Localization;
using OrchardCore.Navigation;
using OrchardCoreContrib.Navigation;

namespace OrchardCoreContrib.Tests.Navigation;

public class NavigationProviderTests
{
    [Fact]
    public void ShouldBuildNavigation()
    {
        // Arrange
        var navigationProvider = new MainMenu();
        var navigationBuilder = new NavigationBuilder();

        // Act
        navigationProvider.BuildNavigation(navigationBuilder);

        // Assert
        var menuItems = navigationBuilder.Build();

        Assert.Equal("Main Menu", navigationProvider.Name);
        Assert.NotEmpty(menuItems);
        Assert.Equal(3, menuItems.Count);
    }

    internal class MainMenu : NavigationProvider
    {
        public override string Name => "Main Menu";

        public override void BuildNavigation(NavigationBuilder builder)
        {
            builder.Add(new LocalizedString("Home", "Home"));
            builder.Add(new LocalizedString("About", "About"));
            builder.Add(new LocalizedString("Contact", "Contact"));
        }
    }
}