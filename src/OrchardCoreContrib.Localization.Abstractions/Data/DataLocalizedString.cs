using OrchardCoreContrib.Infrastructure;

namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// A locale specific data string.
    /// </summary>
    public class DataLocalizedString
    {
        /// <summary>
        /// Creates a new <see cref="DataLocalizedString"/>.
        /// </summary>
        /// <param name="name">The name of the string in the resource it was loaded from.</param>
        /// <param name="context">The context of the string in the resource it was loaded from.</param>
        /// <param name="value">The actual string.</param>
        public DataLocalizedString(string name, string context, string value)
            : this(name, context, value, resourceNotFound: false)
        {

        }

        /// <summary>
        /// Creates a new <see cref="DataLocalizedString"/>.
        /// </summary>
        /// <param name="name">The name of the string in the resource it was loaded from.</param>
        /// <param name="context">The context of the string in the resource it was loaded from.</param>
        /// <param name="value">The actual string.</param>
        /// <param name="resourceNotFound">Whether the resource is found or not.</param>
        public DataLocalizedString(string name, string context, string value, bool resourceNotFound)
        {
            Guard.ArgumentNotNull(name, nameof(name));
            Guard.ArgumentNotNull(context, nameof(context));
            Guard.ArgumentNotNull(value, nameof(value));

            Name = name;
            Context = context;
            Value = value;
            ResourceNotFound = resourceNotFound;
        }

        public static implicit operator string(DataLocalizedString dataLocalizedString) => dataLocalizedString?.Value;

        /// <summary>
        /// The name of the string in the resource it was loaded from.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The context of the string in the resource it was loaded from.
        /// </summary>
        public string Context { get; }

        /// <summary>
        /// The actual string.
        /// </summary>
        public string Value { get; }

        /// <summary>
        /// Whether the string was not found in a resource. If <c>true</c>, an alternate string value was used.
        /// </summary>
        public bool ResourceNotFound { get; }

        /// <inheritdoc/>
        public override string ToString() => Value;
    }
}
