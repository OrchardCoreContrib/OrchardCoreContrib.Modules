using Xunit;

namespace OrchardCoreContrib.Localization.Diacritics.Tests
{
    public class AccentDictionaryRecordTests
    {
        [Fact]
        public void ShouldReturnsTrueIfRecordsAreEqual()
        {
            // Arrange
            var record1 = new AccentDictionaryRecord('é', "e");
            var record2 = new AccentDictionaryRecord('é', "e");

            // Act & Assert
            Assert.True(record1 == record2);
            Assert.True(record1.Equals(record2));
        }

        [Fact]
        public void ShouldReturnsFalseIfRecordsAreNotEqual()
        {
            // Arrange
            var record1 = new AccentDictionaryRecord('é', "e");
            var record2 = new AccentDictionaryRecord('è', "e");

            // Act & Assert
            Assert.True(record1 != record2);
            Assert.True(!record1.Equals(record2));
        }
    }
}
