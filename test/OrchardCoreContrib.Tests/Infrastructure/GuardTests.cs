using System;
using System.Collections.Generic;
using OrchardCoreContrib.Infrastructure;
using Xunit;

namespace OrchardCoreContrib.Tests.Infrastructure
{
    public class GuardTests
    {
        [Fact]
        public void ArgumentNotNull_NullableValue_ThrowsArgumentNullException()
        {
            // Arrange
            string name = null;

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull(nameof(name), name));
            Assert.Null(name);
            Assert.Equal(nameof(name), exception.ParamName);
            Assert.Equal($"Value cannot be null. (Parameter '{nameof(name)}')", exception.Message);
        }

        [Theory]
        [InlineData("name", null)]
        [InlineData("name", "")]
        public void ArgumentNotNullOrEmpty_NullableOrEmptyString_ThrowsArgumentNullException(string name, string value)
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => Guard.ArgumentNotNullOrEmpty(name, value));
            Assert.Equal(name, exception.ParamName);
            Assert.Equal($"Value cannot be empty. (Parameter '{name}')", exception.Message);
        }

        [Theory]
        [InlineData("names", null, typeof(ArgumentNullException), "Value cannot be null. (Parameter 'names')")]
        [InlineData("names", new string[] {}, typeof(ArgumentException), "Value cannot be empty. (Parameter 'names')")]
        public void ArgumentNotNullOrEmpty_NullableOrEmptyCollection_ThrowsArgumentNullExceptionOrArgumentException(string name, IEnumerable<string> value, Type expectedException, string expectedExceptionMessage)
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws(expectedException, () => Guard.ArgumentNotNullOrEmpty(name, value));
            Assert.Equal(expectedExceptionMessage, exception.Message);
        }
    }
}
