using LinqToDB;
using LinqToDB.Data;
using System;
using YesSql;

namespace OrchardCoreContrib.Linq
{
    internal class OrchardCoreDataConnectionFactory
    {
        public static DataConnection Create(IStore store)
        {
            var connection = store.Configuration.ConnectionFactory.CreateConnection();
            var providerName = GetDatabaseProviderName(store.Configuration.SqlDialect.Name);
            var dataProvider = DataConnection.GetDataProvider(providerName, connection.ConnectionString);

            return new DataConnection(dataProvider, connection.ConnectionString);
        }

        private static string GetDatabaseProviderName(string dialectName) =>
            dialectName switch
            {
                "SqlServer" => "MicrosoftDataSqlClient",
                "Sqlite" => ProviderName.SQLite,
                "MySql" => ProviderName.MySql,
                "PostgreSql" => ProviderName.PostgreSQL,
                _ => throw new NotSupportedException("The provider name is not supported."),
            };
    }
}
