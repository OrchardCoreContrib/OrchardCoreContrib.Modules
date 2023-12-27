using OrchardCore.ContentManagement.Handlers;
using OrchardCoreContrib.IssueTracker.Models;
using System.Threading.Tasks;

namespace OrchardCoreContrib.IssueTracker.Handlers;
public class IssuePartHandler : ContentPartHandler<IssuePart>
{
    public override Task InitializingAsync(InitializingContentContext context, IssuePart part)
    {
        part.Show = true;

        return Task.CompletedTask;
    }
}