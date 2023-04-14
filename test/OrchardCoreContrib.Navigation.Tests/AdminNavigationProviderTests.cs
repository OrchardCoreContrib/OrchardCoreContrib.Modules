namespace OrchardCoreContrib.Navigation.Tests;

public class AdminNavigationProviderTests
{
    [Fact]
    public void AdminNavigationProviderShouldHasAdminAsName()
    {
        // Arrange
        var navigationProvider = new MyAdminMenu();

        // Act & Assert
        Assert.Equal("Admin", navigationProvider.Name);
    }

    internal class MyAdminMenu : AdminNavigationProvider
    {

    }
}
