namespace OrchardCoreContrib.Data.Migrations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MigrationAttribute : Attribute
{
	public MigrationAttribute(long id)
	{
        Id = id;
    }

    public long Id { get; }
}
