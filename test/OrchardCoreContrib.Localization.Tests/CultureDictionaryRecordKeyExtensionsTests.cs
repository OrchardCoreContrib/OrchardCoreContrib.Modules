using OrchardCore.Localization;
using System.Xml.Linq;
using Xunit;

namespace OrchardCoreContrib.Localization.Tests;

public class CultureDictionaryRecordKeyExtensionsTests
{
    [Fact]
    public void GetMessageId()
    {
        // Arrange
        var key = new CultureDictionaryRecordKey
        {
            MessageId = "MessageId",
            Context = "Context"
        };

        // Act
        var messageId = key.GetMessageId();

        // Assert
        Assert.Equal("MessageId", messageId);
    }

    [Fact]
    public void GetContext()
    {
        // Arrange
        var key = new CultureDictionaryRecordKey
        {
            MessageId = "MessageId",
            Context = "Context"
        };

        // Act
        var context = key.GetContext();

        // Assert
        Assert.Equal("context", context);
    }
}
