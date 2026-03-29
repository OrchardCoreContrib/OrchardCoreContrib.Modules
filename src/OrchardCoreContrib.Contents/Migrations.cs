using OrchardCore.Data.Migration;
using OrchardCore.Environment.Shell;
using OrchardCoreContrib.Contents.Indexes;
using YesSql.Sql;

namespace OrchardCoreContrib.Contents;

public class Migrations(IShellFeaturesManager shellFeaturesManager) : DataMigration
{
    public async Task<int> CreateAsync()
    {
        await SchemaBuilder.CreateMapIndexTableAsync<SharedDraftLinkIndex>(table => table
            .Column<string>("LinkId")
            .Column<string>("ContentItemId", column => column.WithLength(250))
            .Column<string>("Token")
            .Column<DateTime>("ExpirationUtc")
            .Column<string>("CreatedBy")
            .Column<DateTime>("CreatedUtc")
        );

        return 1;
    }

    public async Task<int> UpdateFrom1Async()
    {
        var features = await shellFeaturesManager.GetEnabledFeaturesAsync();

        if (!features.Any(f => f.Id == "OrchardCore.Notifications"))
        {
            await SchemaBuilder.AlterIndexTableAsync<SharedDraftLinkIndex>(table =>
            {
                table.DropIndex("IDX_NotificationIndex_DocumentId");
                table.DropIndex("IDX_NotificationIndex_CreatedBy");
            });
        }

        await SchemaBuilder.AlterIndexTableAsync<SharedDraftLinkIndex>(table => table
            .CreateIndex("IDX_SharedDraftLinkIndex_DocumentId",
                "DocumentId",
                "LinkId",
                "ContentItemId",
                "Token",
                "ExpirationUtc",
                "CreatedBy",
                "CreatedUtc")
        );

        await SchemaBuilder.AlterIndexTableAsync<SharedDraftLinkIndex>(table => table
            .CreateIndex("IDX_SharedDraftLinkIndex_CreatedBy",
                "DocumentId",
                "ContentItemId",
                "CreatedBy",
                "CreatedUtc")
        );

        return 2;
    }
}
