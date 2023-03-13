using System;

namespace OrchardCoreContrib.Infrastructure
{
    /// <summary>
    /// Represents an argument checker for <see cref="String"/>.
    /// </summary>
    public static partial class Guard
    {
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
    }
}
