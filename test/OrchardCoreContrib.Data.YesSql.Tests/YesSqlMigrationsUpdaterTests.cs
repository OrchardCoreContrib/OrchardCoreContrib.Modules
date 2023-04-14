using Moq;
using OrchardCoreContrib.Data.YesSql.Migrations;
using System.Data.Common;
using YesSql;

namespace OrchardCoreContrib.Data.YesSql.Tests;

public class YesSqlMigrationsUpdaterTests
{
    [Fact]
    public async Task SchemaBuilderPropertyShouldBeSet()
    {
        // Arrange
        var migration = new Migration1();
        var session = new Mock<ISession>();
        session
            .Setup(s => s.BeginTransactionAsync())
            .Returns(() => Task.FromResult(Mock.Of<DbTransaction>()));

        var store = new Mock<IStore>();
        store
            .Setup(s => s.Configuration)
            .Returns(() => Mock.Of<IConfiguration>());

        var migrationUpdater = new YesSqlMigrationsUpdater(session.Object, store.Object);

        // Act
        await migrationUpdater.MigratingAsync(migration);

        // Assert
        Assert.NotNull(migration.SchemaBuilder);
    }
}
