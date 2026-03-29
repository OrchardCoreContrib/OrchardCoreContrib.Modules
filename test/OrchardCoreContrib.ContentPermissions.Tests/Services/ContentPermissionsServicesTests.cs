using Microsoft.AspNetCore.Http;
using Moq;
using OrchardCore;
using OrchardCore.ContentManagement;
using OrchardCore.ContentManagement.Metadata;
using OrchardCore.ContentManagement.Metadata.Models;
using OrchardCoreContrib.ContentPermissions.Models;
using System.Security.Claims;
using System.Text.Json.Nodes;

namespace OrchardCoreContrib.ContentPermissions.Services.Tests;

public class ContentPermissionsServicesTests
{
    [Fact]
    public async Task ShouldAccessContent_IfNoRolesAndUsersDisabled()
    {
        // Arrange
        var contentPermissionsServices = new ContentPermissionsServices(
            CreateContentDefinitionManager(), CreateHttpContextAccessor());
        var contentItem = CreateContentItem();

        // Act
        var isAuthorized = await contentPermissionsServices.AuthorizeAsync(contentItem);

        // Assert
        Assert.True(isAuthorized);
    }

    [Fact]
    public async Task ShouldAccessContent_IfRolesContainsAnonymous()
    {
        // Arrange
        var contentPermissionsServices = new ContentPermissionsServices(
            CreateContentDefinitionManager(enableRoles: true), CreateHttpContextAccessor());
        var contentItem = CreateContentItem(roles: [OrchardCoreConstants.Roles.Anonymous]);

        // Act
        var isAuthorized = await contentPermissionsServices.AuthorizeAsync(contentItem);

        // Assert
        Assert.True(isAuthorized);
    }

    [Theory]
    [InlineData(null, false)]
    [InlineData("User1", true)]
    public async Task ShouldAccessContent_IfRolesContainsAuthenticatedAndUserIsAuthenticated(string user, bool expected)
    {
        // Arrange
        var contentPermissionsServices = new ContentPermissionsServices(
            CreateContentDefinitionManager(enableRoles: true), CreateHttpContextAccessor(user));
        var contentItem = CreateContentItem(roles: [OrchardCoreConstants.Roles.Authenticated]);

        // Act
        var isAuthorized = await contentPermissionsServices.AuthorizeAsync(contentItem);

        // Assert
        Assert.Equal(expected, isAuthorized);
    }

    [Theory]
    [InlineData(false, false, new string[] { "Role1", "Role2" }, true)]
    [InlineData(true, false, new string[] { "Role1", "Role2" }, false)]
    [InlineData(false, true, new string[] { "Role1", "Role2" }, false)]
    [InlineData(true, true, new string[] { "Role1", "Role2" }, false)]
    [InlineData(false, false, new string[] { "Role3", "Role4" }, true)]
    [InlineData(true, false, new string[] { "Role3", "Role4" }, true)]
    [InlineData(false, true, new string[] { "Role3", "Role4" }, false)]
    [InlineData(true, true, new string[] { "Role3", "Role4" }, true)]
    public async Task ShouldAccessContent_IfUserContainsRole(bool enableRoles, bool enableUsers, string[] roles, bool expected)
    {
        // Arrange
        var contentPermissionsServices = new ContentPermissionsServices(
            CreateContentDefinitionManager(enableRoles, enableUsers), CreateHttpContextAccessor(roles: roles));
        var contentItem = CreateContentItem(roles: ["Role3"]);

        // Act
        var isAuthorized = await contentPermissionsServices.AuthorizeAsync(contentItem);

        // Assert
        Assert.Equal(expected, isAuthorized);
    }

    [Theory]
    [InlineData(false, false, new string[] { "Role1", "Role2" }, new string[] { "User1", "User2" }, true)]
    [InlineData(true, false, new string[] { "Role1", "Role2" }, new string[] { "User1", "User2" }, false)]
    [InlineData(false, true, new string[] { "Role1", "Role2" }, new string[] { "User1", "User2" }, false)]
    [InlineData(true, true, new string[] { "Role1", "Role2" }, new string[] { "User1", "User2" }, false)]
    [InlineData(false, false, new string[] { "Role3", "Role4" }, new string[] { "User3", "User4" }, true)]
    [InlineData(true, false, new string[] { "Role3", "Role4" }, new string[] { "User3", "User4" }, false)]
    [InlineData(false, true, new string[] { "Role3", "Role4" }, new string[] { "User3", "User4" }, true)]
    [InlineData(true, true, new string[] { "Role3", "Role4" }, new string[] { "User3", "User4" }, true)]
    public async Task ShouldAccessContent_IfUserMatch(bool enableRoles, bool enableUsers, string[] roles, string[] users, bool expected)
    {
        // Arrange
        var contentPermissionsServices = new ContentPermissionsServices(
            CreateContentDefinitionManager(enableRoles, enableUsers), CreateHttpContextAccessor("User3"));
        var contentItem = CreateContentItem(roles, users);

        // Act
        var isAuthorized = await contentPermissionsServices.AuthorizeAsync(contentItem);

        // Assert
        Assert.Equal(expected, isAuthorized);
    }

    private static IContentDefinitionManager CreateContentDefinitionManager(bool enableRoles = false, bool enableUsers = false)
    {
        var contentTypePartDeinitions = new List<ContentTypePartDefinition>
        {
            new(Constants.ContentPermissionsPartName, new ContentPartDefinition(Constants.ContentPermissionsPartName), JsonObject.Parse($$"""
            {
                "ContentPermissionsPartSettings": {
                    "EnableRoles": {{enableRoles.ToString().ToLower()}},
                    "EnableUsers": {{enableUsers.ToString().ToLower()}}
                }
            }
            """).AsObject())
        };
        var contentDefinitionManagerMock = new Mock<IContentDefinitionManager>();

        contentDefinitionManagerMock.Setup(manager => manager.GetTypeDefinitionAsync(It.IsAny<string>()))
            .ReturnsAsync(new ContentTypeDefinition("ContentType", string.Empty, contentTypePartDeinitions, null));

        return contentDefinitionManagerMock.Object;
    }

    private static IHttpContextAccessor CreateHttpContextAccessor(string user = null, string[] roles = null)
    {
        var httpContextAccessorMock = new Mock<IHttpContextAccessor>();

        httpContextAccessorMock.Setup(accessor => accessor.HttpContext)
            .Returns(() =>
            {
                ClaimsIdentity claimsIdentity = null;
                if (user is null)
                {
                    claimsIdentity = new ClaimsIdentity();
                }
                else
                {
                    claimsIdentity = new ClaimsIdentity("Test Authentication");
                    claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user));
                }

                if (roles is not null)
                {
                    foreach (var role in roles)
                    {
                        claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, role));
                    }
                }

                return new DefaultHttpContext
                {
                    User = new ClaimsPrincipal(claimsIdentity)
                };
            });

        return httpContextAccessorMock.Object;
    }

    private static ContentItem CreateContentItem(string[] roles = null, string[] users = null)
    {
        var contentItem = new ContentItem();
        contentItem.Weld(new ContentPermissionsPart
        {
            Roles = roles,
            Users = users
        });

        return contentItem;
    }
}
