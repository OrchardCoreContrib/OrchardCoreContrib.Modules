using OrchardCoreContrib.Data.Migrations;
using YesSql.Sql;

namespace OrchardCoreContrib.Data.YesSql.Migrations;

public abstract class YesSqlMigration : Migration
{
	public ISchemaBuilder SchemaBuilder { get; internal set; }
}
