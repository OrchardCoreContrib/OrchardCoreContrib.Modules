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
        /// <param name="argumentName">The name of the tested value.</param>
        /// <param name="argumentValue">The value to be tested.</param>
        public static void ArgumentNotNull(string argumentName, object argumentValue)
        {
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException("Value cannot be empty.", nameof(argumentName));
            }

            if (argumentValue is null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Throws an exception if the given string value is <see langword="null" /> or <see cref="string.Empty"/>.
        /// </summary>
        /// <param name="argumentName">The name of the tested value.</param>
        /// <param name="argumentValue">The string value to be tested.</param>
        public static void ArgumentNotNullOrEmpty(string argumentName, string argumentValue)
        {
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException("Value cannot be empty.", nameof(argumentName));
            }

            if (string.IsNullOrEmpty(argumentValue))
            {
                throw new ArgumentException("Value cannot be empty.", argumentName);
            }
        }

        /// <summary>
        /// Throws an exception if the given collection is <see langword="null" /> or empty.
        /// </summary>
        /// <param name="argumentName">The name of the tested collection.</param>
        /// <param name="argumentValue">The collection to be tested.</param>
        public static void ArgumentNotNullOrEmpty(string argumentName, IEnumerable<object> argumentValue)
        {
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentException("Value cannot be empty.", nameof(argumentName));
            }

            if (argumentValue is null)
            {
                throw new ArgumentNullException(argumentName);
            }

            if (argumentValue.Count() == 0)
            {
                throw new ArgumentException("Value cannot be empty.", argumentName);
            }
        }
    }
}
