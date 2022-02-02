using Microsoft.OpenApi.Models;

namespace OrchardCoreContrib.OpenApi.Abstractions
{
    /// <summary>
    /// Represents a contract for defining OpenAPI object.
    /// </summary>
    public interface IOpenApiDefinition
    {
        /// <summary>
        /// Gets the application name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the version OpenAPI document.
        /// </summary>
        string Version { get; }

        /// <summary>
        /// Gets the application description.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets a metadata for OpenAPI object.
        /// </summary>
        OpenApiInfo Info { get; }
    }
}
