namespace OrchardCoreContrib.Data.Migrations;

public class MigrationDictionaryRecord
{
    public MigrationDictionaryRecord(long id, IMigration migration)
    {
        Id = id;
        Migration = migration;
    }

    public long Id { get; }

    public IMigration Migration { get; }
}
