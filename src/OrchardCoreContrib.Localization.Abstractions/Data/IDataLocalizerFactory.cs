namespace OrchardCoreContrib.Localization.Data
{
    /// <summary>
    /// Represents a contract for creating dynamic data localizers.
    /// </summary>
    public interface IDataLocalizerFactory
    {
        /// <summary>
        /// Creates a <see cref="IDataLocalizer"/>.
        /// </summary>
        IDataLocalizer Create();
    }
}
