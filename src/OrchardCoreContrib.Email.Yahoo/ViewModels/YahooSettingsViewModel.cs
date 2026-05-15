using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.Email.Yahoo.ViewModels;

/// <summary>
/// Represents a view model for <see cref="YahooSettings"/>.
/// </summary>
public class YahooSettingsViewModel
{
    /// <summary>
    /// Gets or sets an email recipients.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public string To { get; set; }

    /// <summary>
    /// Gets or sets an email sender.
    /// </summary>
    public string Sender { get; set; }

    /// <summary>
    /// Gets or sets an email recipients that will receieve a copy of the email and they will not be visible to anyone.
    /// </summary>
    public string Bcc { get; set; }

    /// <summary>
    /// Gets or sets an email recipients that will receieve a copy of the email and they will visible to all other recipients.
    /// </summary>
    public string Cc { get; set; }

    /// <summary>
    /// Gets or sets to whome the email will reply to.
    /// </summary>
    public string ReplyTo { get; set; }

    /// <summary>
    /// Gets or sets an email subject.
    /// </summary>
    public string Subject { get; set; }

    /// <summary>
    /// Gets or sets an email body.
    /// </summary>
    public string Body { get; set; }
}
