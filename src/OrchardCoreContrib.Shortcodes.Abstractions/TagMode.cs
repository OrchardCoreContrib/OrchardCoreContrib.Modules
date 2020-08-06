namespace OrchardCoreContrib.Shortcodes
{
    /// <summary>
    /// Defines the mode in which an element should render.
    /// </summary>
    public enum TagMode
    {
        /// <summary>
        /// Include both start and end tags.
        /// </summary>
        StartTagAndEndTag,

        /// <summary>
        /// A self-closed tag.
        /// </summary>
        SelfClosing,

        /// <summary>
        /// Only a start tag.
        /// </summary>
        StartTagOnly
    }
}
