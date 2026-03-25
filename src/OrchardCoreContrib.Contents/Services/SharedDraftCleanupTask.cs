using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OrchardCore.BackgroundTasks;

namespace OrchardCoreContrib.Contents.Services;

[BackgroundTask(Schedule = "0 0 * * *", Description = "Shared Draft Cleanup Background Service")]
public class SharedDraftCleanupTask(ISharedDraftLinkService linkService) : IBackgroundTask
{
    public async Task DoWorkAsync(IServiceProvider serviceProvider, CancellationToken cancellationToken)
    {
        var count = await linkService.CleanupExpiredLinksAsync();

        if (count > 0)
        {
            var logger = serviceProvider.GetRequiredService<ILogger<SharedDraftCleanupTask>>();

            logger.LogInformation("Cleaned up {Count} expired draft links.", count);
        }
    }
}

