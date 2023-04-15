namespace OrchardCoreContrib.Data.Migrations;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
public class MigrationAttribute : Attribute
{
	public MigrationAttribute(long id, bool skip = false)
	{
        Id = id;
        Skip = skip;
    }

    public long Id { get; }

    public bool Skip { get; set; }
}
