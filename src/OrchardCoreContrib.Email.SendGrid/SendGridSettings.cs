using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.Email.SendGrid
{
    /// <summary>
    /// Represents a settings for SendGrid.
    /// </summary>
    public class SendGridSettings
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string DefaultSender { get; set; }

        /// <summary>
        /// Gets or sets the API key.
        /// </summary>
        public string ApiKey { get; set; }
    }
}
