using System;

namespace OrchardCoreContrib.Infrastructure
{
    /// <summary>
    /// Represents an argument checker.
    /// </summary>
    public static partial class Guard
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
    }
}
