using Microsoft.OpenApi.Models;

namespace OrchardCoreContrib.OpenApi.Abstractions
{
    /// <summary>
    /// Represents a base class for OpenAPI definition.
    /// </summary>
    public abstract class OpenApiDefinition : IOpenApiDefinition
    {
        /// <inheritdoc/>
        public abstract string Name { get; }

        /// <inheritdoc/>
        public virtual string Description { get; }

        /// <inheritdoc/>
        public abstract string Version { get; }

        /// <inheritdoc/>
        public OpenApiInfo Info => new OpenApiInfo
        {
            Version = Version,
            Title = Name,
            Description = Description
        };
    }
}
