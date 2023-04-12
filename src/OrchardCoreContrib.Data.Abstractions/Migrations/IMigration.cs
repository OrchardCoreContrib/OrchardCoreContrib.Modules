namespace OrchardCoreContrib.Data.Migrations;

public interface IMigration
{
    void Up();

    void Down();
}
