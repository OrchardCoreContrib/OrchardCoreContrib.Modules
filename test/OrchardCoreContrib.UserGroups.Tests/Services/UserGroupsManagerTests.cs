using OrchardCoreContrib.UserGroups.Helpers.Tests;
using OrchardCoreContrib.UserGroups.Models;

namespace OrchardCoreContrib.UserGroups.Services.Tests;

public class UserGroupsManagerTests
{
    [Fact]
    public async Task CreateUserGroup_ValidGroup_CreatesGroup()
    {
        // Arrange
        var (userGroupsManager, documentManager) = UserGroupsManagerHelper.Create();
        var userGroup = new UserGroup
        {
            Name = "TestGroup",
            Description = "Test Description"
        };

        // Act
        var result = await userGroupsManager.CreateAsync(userGroup);

        // Assert
        Assert.True(result.Succeeded);

        var userGroupDocument = await documentManager.GetOrCreateImmutableAsync();
        Assert.Single(userGroupDocument.UserGroups);
    }

    [Fact]
    public async Task CreateUserGroup_InvalidGroupName_DoesNotCreateGroup()
    {
        // Arrange
        var (userGroupsManager, documentManager) = UserGroupsManagerHelper.Create();
        var userGroup = new UserGroup
        {
            Name = "|TestGroup?",
            Description = "Test Description"
        };

        // Act
        var result = await userGroupsManager.CreateAsync(userGroup);

        // Assert
        Assert.False(result.Succeeded);

        var errors = result.Errors;
        Assert.Single(errors);
        Assert.Equal("Invalid user group name.", errors.Single().Description);

        var userGroupDocument = await documentManager.GetOrCreateImmutableAsync();
        Assert.Empty(userGroupDocument.UserGroups);
    }

    [Fact]
    public async Task CreateUserGroup_ExistGroup_ReturnsError()
    {
        // Arrange    
        var userGroup = new UserGroup
        {
            Name = "TestGroup",
            Description = "Test Description"
        };
        var (userGroupsManager, documentManager) = UserGroupsManagerHelper.Create(userGroup);

        // Act
        var result = await userGroupsManager.CreateAsync(userGroup);

        // Assert
        Assert.False(result.Succeeded);

        var errors = result.Errors;
        Assert.Single(errors);

        Assert.Equal($"The user group '{userGroup.Name}' already exists.", errors.Single().Description);

        var userGroupDocument = await documentManager.GetOrCreateImmutableAsync();
        Assert.NotEmpty(userGroupDocument.UserGroups);
    }

    [Fact]
    public async Task DeleteUserGroup_ExistGroup_DeletesGroup()
    {
        // Arrange
        var userGroup = new UserGroup
        {
            Name = "TestGroup",
            Description = "Test Description"
        };
        var (userGroupsManager, documentManager) = UserGroupsManagerHelper.Create(userGroup);

        // Act
        var result = await userGroupsManager.DeleteAsync(userGroup.Name);

        // Assert
        Assert.True(result.Succeeded);

        var userGroupDocument = await documentManager.GetOrCreateImmutableAsync();
        Assert.Empty(userGroupDocument.UserGroups);
    }

    [Fact]
    public async Task DeleteUserGroup_NonExistGroup_ReturnsError()
    {
        // Arrange
        var groupName = "TestGroup";
        var (userGroupsManager, _) = UserGroupsManagerHelper.Create();

        // Act
        var result = await userGroupsManager.DeleteAsync(groupName);

        // Assert
        Assert.False(result.Succeeded);

        var errors = result.Errors;
        Assert.Single(errors);
        Assert.Equal($"The user group '{groupName}' does not exist.", errors.Single().Description);
    }

    [Fact]
    public async Task UpdateUserGroup_ExistGroup_UpdatesGroup()
    {
        // Arrange
        var userGroup = new UserGroup
        {
            Name = "TestGroup",
            Description = "Test Description"
        };
        var (userGroupsManager, documentManager) = UserGroupsManagerHelper.Create(userGroup);
        userGroup.Description = "Updated Description";

        // Act
        var result = await userGroupsManager.UpdateAsync(userGroup);

        // Assert
        Assert.True(result.Succeeded);

        var userGroupDocument = await documentManager.GetOrCreateImmutableAsync();
        Assert.Equal(userGroup.Description, userGroupDocument.UserGroups.Single().Value.Description);
    }

    [Fact]
    public async Task UpdateUserGroup_NonExistGroup_ReturnsError()
    {
        // Arrange
        var userGroup = new UserGroup
        {
            Name = "TestGroup",
            Description = "Test Description"
        };
        var (userGroupsManager, documentManager) = UserGroupsManagerHelper.Create(userGroup);
        userGroup.Name = "Updated Group";
        userGroup.Description = "Updated Description";

        // Act
        var result = await userGroupsManager.UpdateAsync(userGroup);

        // Assert
        Assert.False(result.Succeeded);

        var errors = result.Errors;
        Assert.Single(errors);
        Assert.Equal($"The user group '{userGroup.Name}' does not exist.", errors.ElementAt(0).Description);

        var userGroupDocument = await documentManager.GetOrCreateImmutableAsync();
        Assert.NotEmpty(userGroupDocument.UserGroups);
    }
}
