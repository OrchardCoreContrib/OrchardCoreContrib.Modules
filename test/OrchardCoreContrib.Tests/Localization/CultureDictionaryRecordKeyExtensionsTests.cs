using OrchardCore.Localization;
using Xunit;

namespace OrchardCoreContrib.Localization.Tests
{
    public class CultureDictionaryRecordKeyExtensionsTests
    {
        [Fact]
        public void GetMessageId()
        {
            // Arrange
            var key = new CultureDictionaryRecordKey("MessageId", "Context");

            // Act
            var messageId = key.GetMessageId();

            // Assert
            Assert.Equal("MessageId", messageId);
        }

        [Fact]
        public void GetContext()
        {
            // Arrange
            var key = new CultureDictionaryRecordKey("MessageId", "Context");

            // Act
            var context = key.GetContext();

            // Assert
            Assert.Equal("context", context);
        }
    }
}
