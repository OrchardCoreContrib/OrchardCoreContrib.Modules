using OrchardCore.Entities;

namespace OrchardCoreContrib.Contents.Models;

public class SharedDraftLink : Entity
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string ContentItemId { get; set; }

    public string Token { get; set; }

    public DateTime ExpirationUtc { get; set; }

    public string CreatedBy { get; set; }

    public DateTime CreatedUtc { get; set; }
}

