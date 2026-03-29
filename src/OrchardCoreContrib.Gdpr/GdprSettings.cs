namespace OrchardCoreContrib.Gdpr;

/// <summary>
/// Represents a GDPR settings
/// </summary>
public class GdprSettings
{
    private static readonly string DefaultSummary = "Use this page to summarize your privacy and cookie use policy.";

    private static readonly string DefaultDetail = "Use this page to detail your site's privacy policy.";

    /// <summary>
    /// Gets or sets a summary that will be used in the cookie consent UI.
    /// </summary>
    public string Summary { get; set; } = DefaultSummary;

    /// <summary>
    /// Gets or sets a detail that will be used in the privacy policy page.
    /// </summary>
    public string Detail { get; set; } = DefaultDetail;
}
