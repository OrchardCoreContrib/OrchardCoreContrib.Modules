using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Garnet",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.0.0"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Garnet",
    Name = "Garnet",
    Description = "Garnet configuration support.",
    Category = "Distributed"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.Garnet.Cache",
    Name = "Garnet Cache",
    Description = "Distributed cache using Garnet.",
    Dependencies = ["OrchardCoreContrib.Garnet"],
    Category = "Distributed"
)]
