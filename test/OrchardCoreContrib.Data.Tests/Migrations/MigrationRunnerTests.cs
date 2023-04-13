using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using OrchardCoreContrib.Data.Migrations;
using OrchardCoreContrib.Module1.Migrations;
using OrchardCoreContrib.Module2.Migrations;
using YesSql;

namespace OrchardCoreContrib.Data.Tests.Migrations;

public class MigrationRunnerTests
{
    [Theory]
    [InlineData("OrchardCoreContrib.Module1", 3)]
    [InlineData("OrchardCoreContrib.Module2", 1)]
    public async Task ShouldRunAllModuleMigrations(string moduleId, int expectedAppliedMigrations)
    {
        // Arrange
        var migrationLoader = GetMigrationLoader();
        var session = GetSession();

        var migrationRunner = new MigrationRunner(migrationLoader, session, NullLogger<MigrationRunner>.Instance);

        // Act
        await migrationRunner.MigrateAsync(moduleId);

        // Assert
        var migrations = await session.GetAsync<MigrationsHistory>(null);

        Assert.Single(migrations);
        Assert.Equal(expectedAppliedMigrations, migrations.Single().Migrations.Count);
    }

    private static IMigrationLoader GetMigrationLoader()
    {
        var services = new ServiceCollection();

        services.AddScoped<IMigration, Migration1>();
        services.AddScoped<IMigration, Migration2>();
        services.AddScoped<IMigration, Migration3>();
        services.AddScoped<IMigration, Migration4>();
        services.AddScoped<IMigrationLoader, MigrationLoader>();

        var serviceProvider = services.BuildServiceProvider();

        return serviceProvider.GetService<IMigrationLoader>();
    }

    private static ISession GetSession()
    {
        var records = new List<object>();
        var session = new Mock<ISession>();
        session
            .Setup(s => s.Save(It.IsAny<object>(), It.IsAny<bool>(), It.IsAny<string>()))
            .Callback<object, bool, string>((obj, _, _) =>
            {
                records.RemoveAll(r => r.GetType() == obj.GetType());
                records.Add(obj);
            });
        session
            .Setup(s => s.GetAsync<MigrationsHistory>(It.IsAny<long[]>(), It.IsAny<string>()))
            .Returns<long[], string>((ids, _) => Task.FromResult(records.OfType<MigrationsHistory>()));
        session
            .Setup(s => s.Query(It.IsAny<string>()))
            .Returns<string>(_ =>
            {
                var query = new Mock<IQuery>();
                query
                    .Setup(q => q.For<MigrationsHistory>(It.IsAny<bool>()))
                    .Returns<bool>(_ =>
                    {
                        var queryOfT = new Mock<IQuery<MigrationsHistory>>();
                        queryOfT
                            .Setup(q => q.FirstOrDefaultAsync())
                            .Returns(() => Task.FromResult(default(MigrationsHistory)));

                        return queryOfT.Object;
                    });

                return query.Object;
            });
        session
            .Setup(s => s.CancelAsync())
            .Returns(async () =>
            {
                records.Clear();

                await Task.CompletedTask;
            });

        return session.Object;
    }
}
