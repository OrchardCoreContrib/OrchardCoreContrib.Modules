using Microsoft.Extensions.Localization;
using Moq;

namespace OrchardCoreContrib.Templating.Liquid.Services.Tests;

public class LiquidTemplateEngineTests
{
    private readonly LiquidTemplateEngine _templateEngine;

    public LiquidTemplateEngineTests()
    {
        var stringLocalizerMock = new Mock<IStringLocalizer<LiquidTemplateEngine>>();

        stringLocalizerMock.Setup(localizer => localizer[It.IsAny<string>()])
            .Returns((string k) => new LocalizedString(k, k));

        stringLocalizerMock.Setup(localizer => localizer[It.IsAny<string>(), It.IsAny<object[]>()])
            .Returns((string k, object[] a) => new LocalizedString(k, string.Format(k, a)));

        _templateEngine = new LiquidTemplateEngine(stringLocalizerMock.Object);
    }

    [Fact]
    public async Task RenderAsync_ShouldRenderTemplate_WhenTemplateIsValid()
    {
        // Arrange
        var template = "Hello {{ name }}";
        var context = new TemplateContext(new { name = "World" });

        // Act
        var result = await _templateEngine.RenderAsync(template, context);

        // Assert
        Assert.Equal("Hello World", result.Value);
    }

    [Fact]
    public async Task RenderAsync_ShouldThrowTemplateRenderException_WhenTemplateParsingFails()
    {
        // Arrange
        var template = "{{";
        var context = new TemplateContext();

        // Act
        var result = await _templateEngine.RenderAsync(template, context);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.StartsWith("Liquid parsing failed:", result.Errors.Single().Message);
    }
}
