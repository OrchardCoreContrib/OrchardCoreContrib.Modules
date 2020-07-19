namespace OrchardCoreContrib.Users.Models
{
    /// <summary>
    /// Represents the settings for the impersonation feature.
    /// </summary>
    public class ImpersonationSettings
    {
        /// <summary>
        /// Gets or sets whether the user impersonation is enabled or not.
        /// </summary>
        public bool EnableImpersonation { get; set; }

        /// <summary>
        /// Gets or sets whether the impersonation process should be ended or not.
        /// </summary>
        public bool EndImpersonation { get; set; }
    }
}
