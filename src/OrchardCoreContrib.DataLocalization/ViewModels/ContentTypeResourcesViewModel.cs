using OrchardCoreContrib.DataLocalization.Models;

namespace OrchardCoreContrib.DataLocalization.ViewModels;

public class ContentTypeResourcesViewModel
{
    public IEnumerable<string> ResourcesNames { get; set; }

    public IEnumerable<Translation> Translations { get; set; }

    public string SelectedCulture { get; set; }

    public bool HasCulture => !string.IsNullOrEmpty(SelectedCulture);
}
