using Microsoft.Extensions.Localization;
using Moq;

namespace OrchardCoreContrib.Templating.Razor.Services.Tests;

public class RazorTemplateEngineTests
{
    private readonly RazorTemplateEngine _templateEngine;

    public RazorTemplateEngineTests()
    {
        var stringLocalizerMock = new Mock<IStringLocalizer<RazorTemplateEngine>>();

        stringLocalizerMock.Setup(localizer => localizer[It.IsAny<string>()])
            .Returns((string k) => new LocalizedString(k, k));

        stringLocalizerMock.Setup(localizer => localizer[It.IsAny<string>(), It.IsAny<object[]>()])
            .Returns((string k, object[] a) => new LocalizedString(k, string.Format(k, a)));

        _templateEngine = new RazorTemplateEngine(stringLocalizerMock.Object);
    }

    [Fact]
    public async Task RenderAsync_ShouldRenderTemplate_WhenTemplateIsValid()
    {
        // Arrange
        var template = "Hello @Model.Name";
        var context = new TemplateContext(new { Name = "World" });

        // Act
        var result = await _templateEngine.RenderAsync(template, context);

        // Assert
        Assert.Equal("Hello World", result.Value);
    }

    [Fact]
    public async Task RenderAsync_ShouldThrowTemplateRenderException_WhenTemplateParsingFails()
    {
        // Arrange
        var template = "@{ var x = ; }";
        var context = new TemplateContext();

        // Act
        var result = await _templateEngine.RenderAsync(template, context);

        // Assert
        Assert.NotEmpty(result.Errors);
        Assert.StartsWith("Rendering razor template failed:", result.Errors.Single().Message);
    }
}
