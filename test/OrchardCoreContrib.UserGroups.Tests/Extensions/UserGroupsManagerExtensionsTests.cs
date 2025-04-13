using Moq;
using OrchardCore.Documents;
using OrchardCoreContrib.UserGroups.Helpers.Tests;
using OrchardCoreContrib.UserGroups.Models;
using OrchardCoreContrib.UserGroups.Services;

namespace OrchardCoreContrib.UserGroups.Extensions.Tests;

public class UserGroupsManagerExtensionsTests
{
    [Fact]
    public async Task GetUserGroupNamesAsync_ShouldReturnUserGroupNames()
    {
        // Arrange
        var userGroup1 = new UserGroup { Name = "Group1" };
        var userGroup2 = new UserGroup { Name = "Group2" };
        var (userGroupsManager, _) = UserGroupsManagerHelper.Create(userGroup1, userGroup2);

        // Act
        var result = await userGroupsManager.GetUserGroupNamesAsync();

        // Assert
        Assert.Equal([userGroup1.Name, userGroup2.Name], result);
    }

    [Fact]
    public async Task CreateAsync_ShouldCreatesUserGroup()
    {
        // Arrange
        var groupName = "TestGroup";
        var (userGroupsManager, documentManager) = UserGroupsManagerHelper.Create();

        // Act
        var result = await userGroupsManager.CreateAsync(groupName, string.Empty);

        // Assert
        Assert.True(result.Succeeded);
        
        var userGroupDocument = await documentManager.GetOrCreateImmutableAsync();
        Assert.NotEmpty(userGroupDocument.UserGroups);
    }
}
