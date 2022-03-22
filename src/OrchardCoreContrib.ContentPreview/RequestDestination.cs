namespace OrchardCoreContrib.ContentPreview
{
    /// <summary>
    /// Represents an enum like for request destination.
    /// </summary>
    public class RequestDestination
    {
        /// <summary>
        /// Request comes from document aka page.
        /// </summary>
        public readonly static string Document = "document";

        /// <summary>
        /// Request comes from IFrame.
        /// </summary>
        public readonly static string Iframe = "iframe";
    }
}
