using Microsoft.Extensions.Logging;
using Moq;
using OrchardCore.ContentManagement;
using OrchardCoreContrib.ViewCount.Models;

namespace OrchardCoreContrib.ViewCount.Services.Tests;

public class ViewCountServiceTests
{
    [Fact]
    public async Task ViewAsync_ConcurrentCalls_WithSemaphore_AllIncrementsAreApplied()
    {
        // Arrange
        const int concurrentCalls = 20;
        var contentItem = new ContentItem();
        contentItem.Weld(new ViewCountPart { Count = 0 });

        var contentManagerMock = new Mock<IContentManager>();
        contentManagerMock
            .Setup(m => m.UpdateAsync(It.IsAny<ContentItem>()))
            .Returns(async () => await Task.Delay(10).ConfigureAwait(false));

        var service = new ViewCountService(
            contentManagerMock.Object,
            [],
            Mock.Of<ILogger<ViewCountService>>());

        // Act
        var tasks = Enumerable.Range(0, concurrentCalls)
            .Select(_ => Task.Run(() => service.ViewAsync(contentItem)))
            .ToArray();

        await Task.WhenAll(tasks);

        // Assert
        var count = contentItem.As<ViewCountPart>().Count;
        Assert.Equal(concurrentCalls, count);
    }
}
