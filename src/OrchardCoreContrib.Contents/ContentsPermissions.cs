using OrchardCore.Security.Permissions;

namespace OrchardCoreContrib.Contents;

public sealed class ContentsPermissions
{
    public static readonly Permission ShareDraftContent = new("ShareDraftContent", "Share draft content items");
}
