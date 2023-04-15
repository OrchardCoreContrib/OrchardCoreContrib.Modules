namespace OrchardCoreContrib.Data.Migrations;

public class MigrationDictionaryRecord
{
    private readonly Lazy<IMigration> _migration;

    public MigrationDictionaryRecord(long id, bool skip, IMigration migration)
    {
        Id = id;
        Skip = skip;
        _migration = new Lazy<IMigration>(migration);
    }

    public long Id { get; }

    public bool Skip { get; }

    public IMigration Migration => _migration.Value;
}
