using Microsoft.Extensions.Localization;
using Moq;
using OrchardCore.Documents;
using OrchardCoreContrib.UserGroups.Models;
using OrchardCoreContrib.UserGroups.Services;

namespace OrchardCoreContrib.UserGroups.Helpers.Tests;

internal static class UserGroupsManagerHelper
{
    public static (UserGroupsManager, IDocumentManager<UserGroupDocument>) Create(params UserGroup[] groups)
    {
        var internalUserGroupDocument = new UserGroupDocument
        {
            UserGroups = new Dictionary<string, UserGroup>(StringComparer.OrdinalIgnoreCase)
        };
        if (groups is not null)
        {
            foreach (var group in groups)
            {
                internalUserGroupDocument.UserGroups.Add(group.Name, group);
            }
        }

        var documentManagerMock = new Mock<IDocumentManager<UserGroupDocument>>();
        documentManagerMock.Setup(m => m.GetOrCreateImmutableAsync(null))
            .ReturnsAsync(internalUserGroupDocument);
        documentManagerMock.Setup(m => m.GetOrCreateMutableAsync(null))
            .ReturnsAsync(internalUserGroupDocument);
        documentManagerMock.Setup(m => m.UpdateAsync(It.IsAny<UserGroupDocument>(), null))
            .Callback<UserGroupDocument, Func<UserGroupDocument, Task>>((d, _) =>
            {
                foreach (var group in d.UserGroups)
                {
                    internalUserGroupDocument.UserGroups.TryAdd(group.Key, group.Value);
                }
            });
        var stringLocalizerMock = new Mock<IStringLocalizer<UserGroupsManager>>();
        stringLocalizerMock
            .Setup(localizer => localizer[It.IsAny<string>()])
            .Returns<string>(n => new LocalizedString(n, n));
        stringLocalizerMock
            .Setup(localizer => localizer[It.IsAny<string>(), It.IsAny<object[]>()])
            .Returns<string, object[]>((name, args) => new LocalizedString(name, string.Format(name, args)));

        return (new UserGroupsManager(documentManagerMock.Object, stringLocalizerMock.Object), documentManagerMock.Object);
    }
}
