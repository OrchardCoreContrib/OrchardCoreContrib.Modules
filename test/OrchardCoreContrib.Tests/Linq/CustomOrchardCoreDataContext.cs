using LinqToDB;
using YesSql;

namespace OrchardCoreContrib.Linq.Tests
{
    public class CustomOrchardCoreDataContext : OrchardCoreDataContext
    {
        public CustomOrchardCoreDataContext(IStore store) : base(store)
        {

        }

        public ITable<Document> Document => GetTable<Document>();
    }
}
