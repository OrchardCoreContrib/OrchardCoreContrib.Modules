namespace OrchardCoreContrib.Data.Migrations;

public class MigrationDictionaryRecord
{
    private readonly Lazy<IMigration> _migration;

    public MigrationDictionaryRecord(long id, IMigration migration)
    {
        Id = id;
        _migration = new Lazy<IMigration>(migration);
    }

    public long Id { get; }

    public IMigration Migration => _migration.Value;
}
