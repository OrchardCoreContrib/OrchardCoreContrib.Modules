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
        public void ArgumentNotNullOrEmpty_NullableOrEmptyString_ThrowsArgumentNullOrEmptyException(string name, string value)
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullOrEmptyException>(() => Guard.ArgumentNotNullOrEmpty(name, value));
            Assert.Equal(name, exception.ParamName);
            Assert.Equal($"Value cannot be null or empty. (Parameter '{name}')", exception.Message);
        }

        [Theory]
        [InlineData("names", null)]
        [InlineData("names", new string[] {})]
        public void ArgumentNotNullOrEmpty_NullableOrEmptyCollection_ThrowsArgumentNullOrEmptyException(string name, IEnumerable<string> value)
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullOrEmptyException>(() => Guard.ArgumentNotNullOrEmpty(name, value));
            Assert.Equal($"Value cannot be null or empty. (Parameter '{name}')", exception.Message);
        }
    }
}
