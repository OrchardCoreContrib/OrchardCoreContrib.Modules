using System;
using System.Collections.Generic;
using System.Linq;

namespace OrchardCoreContrib.Infrastructure
{
    /// <summary>
    /// Represents an argument checker.
    /// </summary>
    public static class Guard
    {
        /// <summary>
        /// Throws <see cref="ArgumentNullException"/> if the given value is <see langword="null" />.
        /// </summary>
        /// <param name="argumentValue">The value to be tested.</param>
        /// <param name="argumentName">The name of the tested value.</param>
        public static void ArgumentNotNull(object argumentValue, string argumentName)
        {
            if (argumentValue is null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullOrEmptyException"/> if the given string value is <see langword="null" /> or <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="argumentValue">The string value to be tested.</param>
        /// <param name="argumentName">The name of the tested value.</param>
        public static void ArgumentNotNullOrEmpty(string argumentValue, string argumentName)
        {
            if (String.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentNullOrEmptyException(argumentName);
            }
        }

        /// <summary>
        /// Throws <see cref="ArgumentNullOrEmptyException"/> if the given collection is <see langword="null" /> or empty.
        /// </summary>
        /// <param name="argumentValue">The collection to be tested.</param>
        /// <param name="argumentName">The name of the tested collection.</param>
        public static void ArgumentNotNullOrEmpty(IEnumerable<object> argumentValue, string argumentName)
        {
            if (argumentValue is null || !argumentValue.Any())
            {
                throw new ArgumentNullOrEmptyException(argumentName);
            }
        }
    }
}
