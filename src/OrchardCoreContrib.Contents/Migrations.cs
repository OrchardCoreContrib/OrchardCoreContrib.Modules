using OrchardCore.Data.Migration;
using OrchardCoreContrib.Contents.Indexes;
using YesSql.Sql;

namespace OrchardCoreContrib.Contents;

public class Migrations : DataMigration
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

        await SchemaBuilder.AlterIndexTableAsync<SharedDraftLinkIndex>(table => table
            .CreateIndex("IDX_NotificationIndex_DocumentId",
                "DocumentId",
                "LinkId",
                "ContentItemId",
                "Token",
                "ExpirationUtc",
                "CreatedBy",
                "CreatedUtc")
        );

        await SchemaBuilder.AlterIndexTableAsync<SharedDraftLinkIndex>(table => table
            .CreateIndex("IDX_NotificationIndex_CreatedBy",
                "DocumentId",
                "ContentItemId",
                "CreatedBy",
                "CreatedUtc")
        );

        return 1;
    }
}
