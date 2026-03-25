using OrchardCoreContrib.Contents.Models;
using YesSql.Indexes;

namespace OrchardCoreContrib.Contents.Indexes;

public class SharedDraftLinkIndexProvider : IndexProvider<SharedDraftLink>
{
    public override void Describe(DescribeContext<SharedDraftLink> context)
    {
        context.For<SharedDraftLinkIndex>()
            .Map(link => new SharedDraftLinkIndex()
            {
                LinkId = link.Id,
                ContentItemId = link.ContentItemId,
                Token = link.Token,
                ExpirationUtc = link.ExpirationUtc,
                CreatedBy = link.CreatedBy,
                CreatedUtc = link.CreatedUtc
            });
    }
}
