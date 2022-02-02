using OrchardCoreContrib.OpenApi.Abstractions;

namespace OrchardCoreContrib.Apis.Swagger
{
    /// <summary>
    /// Respresents a swagger API definition for Orchard Core.
    /// </summary>
    public class SwaggerApiDefinition : OpenApiDefinition
    {
        /// <inheritdoc/>
        public override string Name => "Orchard Core APIs Docs";

        /// <inheritdoc/>
        public override string Version => "v1.0.0";
    }
}
