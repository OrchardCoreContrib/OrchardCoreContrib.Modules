using Microsoft.Extensions.Logging;
using OrchardCore.ContentManagement;
using OrchardCore.Modules;
using OrchardCoreContrib.Infrastructure;
using OrchardCoreContrib.ViewCount.Handlers;
using OrchardCoreContrib.ViewCount.Models;
using System.Data.Common;
using YesSql;
using Dapper;

namespace OrchardCoreContrib.ViewCount.Services;

/// <summary>
/// Provides functionality for tracking and updating view counts on content items.
/// </summary>
/// <remarks>The <see cref="ViewCountService"/> enables retrieval and incrementing of view counts for content
/// items. It coordinates with registered <see cref="IViewCountContentHandler"/> instances to allow custom logic to be
/// executed before and after a view is recorded. This service is typically used to monitor content popularity or
/// engagement.
/// </remarks>
public class ViewCountService(
    IContentManager contentManager,
    ISession session,
    IEnumerable<IViewCountContentHandler> handlers,
    ILogger<ViewCountService> logger) : IViewCountService
{
    /// <inheritdoc/>
    public int GetViewsCount(ContentItem contentItem)
    {
        Guard.ArgumentNotNull(contentItem, nameof(contentItem));

        var viewCountPart = contentItem.As<ViewCountPart>();
        
        return viewCountPart?.Count ?? 0;
    }

    /// <inheritdoc/>
    public async Task ViewAsync(ContentItem contentItem)
    {
        Guard.ArgumentNotNull(contentItem, nameof(contentItem));

        var viewCountPart = contentItem.As<ViewCountPart>()
            ?? throw new InvalidOperationException($"The content item doesn't have a `{nameof(ViewCountPart)}`.");
        var count = viewCountPart.Count;
        var context = new ViewCountContentContext(contentItem, count);

        await handlers.InvokeAsync((handler, context) => handler.ViewingAsync(context), context, logger);

        // Perform atomic increment directly in the database to avoid race conditions
        await IncrementViewCountAtomicallyAsync(contentItem.ContentItemId);

        // Reload the content item to get the updated count
        contentItem = await contentManager.GetAsync(contentItem.ContentItemId);
        viewCountPart = contentItem.As<ViewCountPart>();
        count = viewCountPart?.Count ?? count + 1;

        context = new ViewCountContentContext(contentItem, count);

        await handlers.InvokeAsync((handler, context) => handler.ViewedAsync(context), context, logger);
    }

    private async Task IncrementViewCountAtomicallyAsync(string contentItemId)
    {
        var dialect = session.Store.Configuration.SqlDialect;
        var tablePrefix = session.Store.Configuration.TablePrefix;
        var schema = session.Store.Configuration.Schema;
        var documentTable = $"{tablePrefix}{nameof(ContentItem)}Document";

        string sql;

        // Use database-specific JSON update functions for atomic increment
        if (dialect.Name == "SqlServer")
        {
            sql = $@"
                UPDATE {dialect.QuoteForTableName(documentTable, schema)}
                SET {dialect.QuoteForColumnName("Content")} = JSON_MODIFY(
                    {dialect.QuoteForColumnName("Content")},
                    '$.ViewCountPart.Count',
                    CAST(ISNULL(JSON_VALUE({dialect.QuoteForColumnName("Content")}, '$.ViewCountPart.Count'), '0') AS INT) + 1
                )
                WHERE JSON_VALUE({dialect.QuoteForColumnName("Content")}, '$.ContentItemId') = @ContentItemId";
        }
        else if (dialect.Name == "Sqlite")
        {
            sql = $@"
                UPDATE {dialect.QuoteForTableName(documentTable, schema)}
                SET {dialect.QuoteForColumnName("Content")} = json_set(
                    {dialect.QuoteForColumnName("Content")},
                    '$.ViewCountPart.Count',
                    CAST(COALESCE(json_extract({dialect.QuoteForColumnName("Content")}, '$.ViewCountPart.Count'), 0) AS INTEGER) + 1
                )
                WHERE json_extract({dialect.QuoteForColumnName("Content")}, '$.ContentItemId') = @ContentItemId";
        }
        else if (dialect.Name == "MySql")
        {
            sql = $@"
                UPDATE {dialect.QuoteForTableName(documentTable, schema)}
                SET {dialect.QuoteForColumnName("Content")} = JSON_SET(
                    {dialect.QuoteForColumnName("Content")},
                    '$.ViewCountPart.Count',
                    CAST(COALESCE(JSON_EXTRACT({dialect.QuoteForColumnName("Content")}, '$.ViewCountPart.Count'), 0) AS UNSIGNED) + 1
                )
                WHERE JSON_EXTRACT({dialect.QuoteForColumnName("Content")}, '$.ContentItemId') = @ContentItemId";
        }
        else if (dialect.Name == "PostgreSql")
        {
            sql = $@"
                UPDATE {dialect.QuoteForTableName(documentTable, schema)}
                SET {dialect.QuoteForColumnName("Content")} = jsonb_set(
                    {dialect.QuoteForColumnName("Content")},
                    '{{ViewCountPart,Count}}',
                    to_jsonb(COALESCE(({dialect.QuoteForColumnName("Content")}->>'ViewCountPart'->>'Count')::int, 0) + 1)
                )
                WHERE {dialect.QuoteForColumnName("Content")}->>'ContentItemId' = @ContentItemId";
        }
        else
        {
            throw new NotSupportedException($"The SQL dialect '{dialect.Name}' is not supported for atomic view count updates.");
        }

        // Use existing session connection. Do not use 'using' or dispose the connection.
        var connection = await session.CreateConnectionAsync();
        
        var commandDefinition = new CommandDefinition(
            commandText: sql,
            parameters: new { ContentItemId = contentItemId },
            transaction: session.CurrentTransaction
        );
        
        await connection.ExecuteAsync(commandDefinition);
    }
}
