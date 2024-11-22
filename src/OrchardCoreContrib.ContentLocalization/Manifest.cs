using OrchardCore.Modules.Manifest;
using ManifestConstants = OrchardCoreContrib.Modules.Manifest.ManifestConstants;

[assembly: Module(
    Name = "Content Localization",
    Author = ManifestConstants.Author,
    Website = ManifestConstants.Website,
    Version = "1.3.1",
    Category = "Internationalization"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.ContentLocalization.LocalizationMatrix",
    Name = "Localization Matrix",
    Description = "Provides a matrix shows the localized content per culture.",
    Dependencies = new[] { "OrchardCoreContrib.ContentLocalization", "OrchardCore.ContentLocalization" },
    Category = "Internationalization"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.ContentLocalization.Transliteration",
    Name = "Transliteration",
    Description = "Provides a type of conversion of a text from one script to another that involves swapping letters.",
    Dependencies = new[] { "OrchardCoreContrib.ContentLocalization" },
    Category = "Internationalization"
)]

[assembly: Feature(
    Id = "OrchardCoreContrib.ContentLocalization.TransliterationLiquid",
    Name = "TransliterationLiquid",
    Description = "Allows the use of existing transliteration services in liquid filters.",
    Dependencies = new[] { "OrchardCoreContrib.ContentLocalization" },
    Category = "Internationalization"
)]