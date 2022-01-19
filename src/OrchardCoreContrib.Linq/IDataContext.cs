using LinqToDB.Data;

namespace OrchardCoreContrib.Linq
{
    /// <summary>
    /// Contract for OrchardCore database context.
    /// </summary>
    public interface IDataContext
    {
        /// <summary>
        /// Get the underlying connection.
        /// </summary>
        public DataConnection Connection { get; }
    }
}
