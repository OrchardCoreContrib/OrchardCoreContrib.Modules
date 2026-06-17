using Microsoft.Extensions.DependencyInjection;
using OrchardCoreContrib.Templating.Liquid.Services;

namespace OrchardCoreContrib.Templating.Liquid.Tests.Extensions;

public class ServiceCollectionExtensionsTests
{
    [Fact]
    public void AddLiquidTemplating_RegistersLiquidTemplateEngineAsSingleton()
    {
        // Arrange
        var services = new ServiceCollection()
            .AddLogging()
            .AddLocalization();

        // Act
        services.AddLiquidTemplating();

        // Assert
        using var provider = services.BuildServiceProvider();

        var first = provider.GetRequiredService<ITemplateEngine>();
        var second = provider.GetRequiredService<ITemplateEngine>();

        Assert.IsType<LiquidTemplateEngine>(first);
        Assert.Same(first, second);
    }
}
