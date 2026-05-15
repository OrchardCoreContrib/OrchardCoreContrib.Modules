using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Settings;
using OrchardCore.Data.Migration;
using OrchardCoreContrib.UserGroups.Indexes;
using YesSql.Sql;

namespace OrchardCoreContrib.UserGroups;

public class Migrations(IContentDefinitionManager contentDefinitionManager) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await contentDefinitionManager.AlterPartDefinitionAsync("UserGroupsListPart", builder => builder
            .Attachable()
            .WithDescription("Provides a way to add user group(s) for your content item."));

        await SchemaBuilder.CreateReduceIndexTableAsync<UserByGroupNameIndex>(table => table
           .Column<string>("GroupName")
           .Column<int>("Count"));

        await SchemaBuilder.AlterIndexTableAsync<UserByGroupNameIndex>(table => table
            .CreateIndex("IDX_UserByGroupNameIndex_GroupName", "GroupName"));

        return 1;
    }
}
