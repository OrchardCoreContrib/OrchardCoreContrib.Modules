namespace OrchardCoreContrib.Data.Migrations;

public abstract class Migration : IMigration
{
    public virtual void Down()
    {
    }

    public abstract void Up();
}
