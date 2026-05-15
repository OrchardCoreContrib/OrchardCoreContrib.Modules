using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Elm Diagnostics",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.6.0",
    Description = "Provides services to handle errors using ASP.NET Core Error Logging Middleware (ELM).",
    Category = "Infrastructure"
)]
