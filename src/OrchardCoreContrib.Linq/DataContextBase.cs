using LinqToDB;
using LinqToDB.Data;

namespace OrchardCoreContrib.Linq
{
    /// <summary>
    /// Represents a base class for database context.
    /// </summary>
    public abstract class DataContextBase : IDataContext
    {
        /// <inheritdoc/>
        public DataConnection Connection { get; protected set; }

        /// <summary>
        /// Gets
        /// </summary>
        /// <typeparam name="TTable"></typeparam>
        public ITable<TTable> GetTable<TTable>() where TTable : class => Connection.GetTable<TTable>();
    }
}
