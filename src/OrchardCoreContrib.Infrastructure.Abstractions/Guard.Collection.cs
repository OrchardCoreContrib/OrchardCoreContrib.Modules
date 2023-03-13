using System.Collections.Generic;
using System.Linq;

namespace OrchardCoreContrib.Infrastructure
{
    /// <summary>
    /// Represents an argument checker for collections.
    /// </summary>
    public static partial class Guard
    {
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
