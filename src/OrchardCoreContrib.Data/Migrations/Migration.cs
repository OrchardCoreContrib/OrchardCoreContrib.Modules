namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a base class for the migration.
/// </summary>
public abstract class Migration : IMigration
{
    /// <inheritdoc/>
    public virtual void Down()
    {
    }

    /// <inheritdoc/>
    public abstract void Up();
}
