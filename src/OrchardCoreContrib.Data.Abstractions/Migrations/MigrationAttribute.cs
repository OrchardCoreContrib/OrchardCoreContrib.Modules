namespace OrchardCoreContrib.Data.Migrations;

/// <summary>
/// Represents a migration attribute.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MigrationAttribute(long id, bool skip = false) : Attribute
{
    /// <summary>
    /// Gets the migration identifier.
    /// </summary>
    public long Id { get; } = id;

    /// <summary>
    /// Checks if the migration should be skipped.
    /// </summary>
    public bool Skip { get; set; } = skip;
}
