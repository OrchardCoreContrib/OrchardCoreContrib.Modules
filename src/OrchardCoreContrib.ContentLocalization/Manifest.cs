using OrchardCore.Modules.Manifest;

[assembly: Module(
    Name = "Content Localization",
    Author = "The Orchard Core Contrib Team",
    Version = "1.0.0",
    Category = "Internationalization"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.ContentLocalization.LocalizationMatrix",
    Name = "Localization Matrix",
    Description = "Provides a matrix shows the localized content per culture.",
    Dependencies = new[] { "OrchardCoreContrib.ContentLocalization", "OrchardCore.ContentLocalization" },
    Category = "Internationalization"
)]
