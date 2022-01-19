using LinqToDB;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using YesSql;
using YesSql.Provider.Sqlite;

namespace OrchardCoreContrib.Linq.Tests
{
    public class OrchardCoreDataContextTests
    {
        private readonly IStore _store;

        public OrchardCoreDataContextTests()
        {
            _store = StoreFactory.CreateAndInitializeAsync(new Configuration()
                .UseSqLite("Data Source=occ.db;Cache=Shared")).Result;
        }

        [Fact]
        public void SimpleQuery()
        {
            // Arrange
            var dbContext = new OrchardCoreDataContext(_store);

            // Act
            var result = dbContext.Aliases
                .OrderBy(index => index.Alias)
                .First();

            // Assert
            Assert.Equal("categories", result.Alias);
        }

        [Fact]
        public void ComplexQuery()
        {
            using var dbContext = new OrchardCoreDataContext(_store);
            var result = from ci in dbContext.ContentItems
                         join r in dbContext.Routes on ci.ContentItemId equals r.ContentItemId
                         where r.Path.StartsWith("tags/", StringComparison.OrdinalIgnoreCase)
                         select ci.DisplayText;

            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task AsyncQuery()
        {
            // Arrange
            var dbContext = new OrchardCoreDataContext(_store);

            // Act
            var result = await dbContext.Aliases.ToListAsync();

            // Assert
            Assert.Equal(3, result.Count);
        }

        [Fact]
        public async Task QueryFromCustomTable()
        {
            // Arrange
            var dbContext = new CustomOrchardCoreDataContext(_store);

            // Act
            var result = await dbContext.Document.ToListAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
        }
    }
}
