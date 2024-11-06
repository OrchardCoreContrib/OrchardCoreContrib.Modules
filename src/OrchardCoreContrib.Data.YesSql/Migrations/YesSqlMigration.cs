using OrchardCoreContrib.Data.Migrations;
using YesSql.Sql;

namespace OrchardCoreContrib.Data.YesSql.Migrations;

/// <summary>
/// Represents a YesSql migration.
/// </summary>
public abstract class YesSqlMigration : Migration
{
	/// <summary>
	/// Gets or sets the schema builder.
	/// </summary>
	public ISchemaBuilder SchemaBuilder { get; internal set; }
}
