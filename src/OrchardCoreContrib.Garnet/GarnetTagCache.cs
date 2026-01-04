using Microsoft.Extensions.Logging;
using OrchardCore.Environment.Cache;
using OrchardCore.Environment.Shell;
using OrchardCore.Modules;
using OrchardCoreContrib.Garnet.Services;

namespace OrchardCoreContrib.Garnet;

/// <summary>
/// Represents a tag cache by Garnet.
/// </summary>
/// <param name="garnetService">The <see cref="IGarnetService"/>.</param>
/// <param name="shellSettings">The <see cref="ShellSettings"/>.</param>
/// <param name="tagRemovedEventHandlers">The <see cref="IEnumerable{ITagRemovedEventHandler}"/>.</param>
/// <param name="logger">The <see cref="ILogger{GarnetTagCache}"/>.</param>
public class GarnetTagCache(
    IGarnetService garnetService,
    ShellSettings shellSettings,
    IEnumerable<ITagRemovedEventHandler> tagRemovedEventHandlers,
    ILogger<GarnetTagCache> logger) : ITagCache
{
    private readonly string _prefix = garnetService.InstancePrefix + shellSettings.Name + ":Tag:";

    /// <inheritdoc/>
    public async Task TagAsync(string key, params string[] tags)
    {
        if (garnetService.Client == null)
        {
            await garnetService.ConnectAsync();
            
            if (garnetService.Client == null)
            {
                logger.LogError("Fails to add the '{KeyName}' to the {PrefixName} tags.", key, _prefix);
                
                return;
            }
        }

        try
        {
            foreach (var tag in tags)
            {
                await garnetService.Client.SetSetAsync(_prefix + tag, key);
            }
        }
        catch (Exception e)
        {
            logger.LogError(e, "Fails to add the '{KeyName}' to the {PrefixName} tags.", key, _prefix);
        }
    }

    /// <inheritdoc/>
    public async Task<IEnumerable<string>> GetTaggedItemsAsync(string tag)
    {
        if (garnetService.Client == null)
        {
            await garnetService.ConnectAsync();
            
            if (garnetService.Client == null)
            {
                logger.LogError("Fails to get '{TagName}' items.", _prefix + tag);
                
                return [];
            }
        }

        try
        {
            var values = await garnetService.Client.SetGetAsync(_prefix + tag);

            if (values == null || values.Length == 0)
            {
                return [];
            }

            return values;
        }
        catch (Exception e)
        {
            logger.LogError(e, "Fails to get '{TagName}' items.", _prefix + tag);
        }

        return [];
    }

    /// <inheritdoc/>
    public async Task RemoveTagAsync(string tag)
    {
        if (garnetService.Client == null)
        {
            await garnetService.ConnectAsync();

            if (garnetService.Client == null)
            {
                logger.LogError("Fails to remove the '{TagName}'.", _prefix + tag);
                
                return;
            }
        }

        var values = await garnetService.Client.SetGetAsync(_prefix + tag);

        if (values == null || values.Length == 0)
        {
            return;
        }

        if (await garnetService.Client.KeyDeleteAsync(_prefix + tag))
        {
            await tagRemovedEventHandlers.InvokeAsync(x => x.TagRemovedAsync(tag, values), logger);
        }
        else
        {
            logger.LogError("Fails to remove the '{TagName}'.", _prefix + tag);
        }
    }
}
