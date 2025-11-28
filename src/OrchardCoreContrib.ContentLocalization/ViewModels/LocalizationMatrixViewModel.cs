namespace OrchardCoreContrib.ContentLocalization.ViewModels;

public class LocalizationMatrixViewModel
{
    public IEnumerable<string> Cultures { get; set; }

    public IEnumerable<string> LocalizationSets { get; set; }
}
