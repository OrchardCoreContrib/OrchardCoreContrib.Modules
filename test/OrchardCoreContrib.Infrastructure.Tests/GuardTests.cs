using OrchardCoreContrib.Infrastructure;
using System;
using System.Collections.Generic;
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
            var exception = Assert.Throws<ArgumentNullException>(() => Guard.ArgumentNotNull(name, nameof(name)));
            Assert.Null(name);
            Assert.Equal(nameof(name), exception.ParamName);
            Assert.Equal($"Value cannot be null. (Parameter '{nameof(name)}')", exception.Message);
        }

        [Theory]
        [InlineData(null, "name")]
        [InlineData("", "name")]
        public void ArgumentNotNullOrEmpty_NullableOrEmptyString_ThrowsArgumentNullOrEmptyException(string value, string name)
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullOrEmptyException>(() => Guard.ArgumentNotNullOrEmpty(value, name));
            Assert.Equal(name, exception.ParamName);
            Assert.Equal($"Value cannot be null or empty. (Parameter '{name}')", exception.Message);
        }

        [Theory]
        [InlineData(null, "names")]
        [InlineData(new string[] { }, "names")]
        public void ArgumentNotNullOrEmpty_NullableOrEmptyCollection_ThrowsArgumentNullOrEmptyException(IEnumerable<string> value, string name)
        {
            // Arrange

            // Act & Assert
            var exception = Assert.Throws<ArgumentNullOrEmptyException>(() => Guard.ArgumentNotNullOrEmpty(value, name));
            Assert.Equal($"Value cannot be null or empty. (Parameter '{name}')", exception.Message);
        }
    }
}
