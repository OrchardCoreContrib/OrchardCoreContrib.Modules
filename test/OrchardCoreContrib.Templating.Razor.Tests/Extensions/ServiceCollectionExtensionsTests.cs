using Microsoft.Extensions.DependencyInjection;
using OrchardCoreContrib.Templating.Razor.Services;

namespace OrchardCoreContrib.Templating.Razor.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddRazorTemplating_RegistersRazorTemplateEngineAsSingleton()
    {
        // Arrange
        var services = new ServiceCollection()
            .AddLogging()
            .AddLocalization();

        // Act
        services.AddRazorTemplating();

        // Assert
        using var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<ITemplateEngine>();
        var second = provider.GetRequiredService<ITemplateEngine>();

        Assert.IsType<RazorTemplateEngine>(first);
        Assert.Same(first, second);
    }
}
