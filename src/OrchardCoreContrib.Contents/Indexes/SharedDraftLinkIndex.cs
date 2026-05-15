using YesSql.Indexes;

namespace OrchardCoreContrib.Contents.Indexes;

public class SharedDraftLinkIndex : MapIndex
{
    public string LinkId { get; set; }

    public string ContentItemId { get; set; }

    public string Token { get; set; }

    public DateTime ExpirationUtc { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedUtc { get; set; }
}
