using Microsoft.Extensions.Options;
using OrchardCoreContrib.Avatars;

namespace OrchardCoreContrib.Gravatar.Services.Tests;

public class GravatarServiceTests
{
    [Fact]
    public void GetAvatar_ShouldThrow_WhenContextIsNull()
    {
        // Arrange
        var service = CreateService(new GravatarOptions { Rating = GravatarRating.G });

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => service.GetAvatar(null!));
    }

    [Fact]
    public void GetAvatar_ShouldThrow_WhenEmailIsNull()
    {
        // Arrange
        var service = CreateService(new GravatarOptions { Rating = GravatarRating.G });
        var context = new AvatarContext { Email = null! };

        // Act & Assert
        Assert.ThrowsAny<ArgumentException>(() => service.GetAvatar(context));
    }

    [Fact]
    public void GetAvatar_ShouldThrow_WhenEmailIsEmpty()
    {
        // Arrange
        var service = CreateService(new GravatarOptions { Rating = GravatarRating.G });
        var context = new AvatarContext { Email = string.Empty };

        // Act & Assert
        Assert.ThrowsAny<ArgumentException>(() => service.GetAvatar(context));
    }

    [Fact]
    public void GetAvatar_ShouldBuildUrl_WithSizeAndRating()
    {
        // Arrange
        var service = CreateService(new GravatarOptions { Rating = GravatarRating.PG });
        var context = new AvatarContext { Email = "test@example.com" };

        // Act
        var result = service.GetAvatar(context, 120);

        // Assert
        Assert.Equal("http://www.gravatar.com/avatar/55502f40dc8b7c769880b10874abc9d0?s=120&r=pg", result);
    }

    [Fact]
    public void GetAvatar_ShouldAppendDefaultImage_WhenConfigured()
    {
        var service = CreateService(new GravatarOptions
        {
            Rating = GravatarRating.G,
            DefaultImage = "identicon"
        });

        var context = new AvatarContext { Email = "test@example.com" };

        var result = service.GetAvatar(context);

        Assert.Equal("http://www.gravatar.com/avatar/55502f40dc8b7c769880b10874abc9d0?s=80&r=g&d=identicon", result);
    }

    [Fact]
    public void GetAvatar_ShouldNotAppendDefaultImage_WhenNotConfigured()
    {
        var service = CreateService(new GravatarOptions
        {
            Rating = GravatarRating.G,
            DefaultImage = ""
        });

        var context = new AvatarContext { Email = "test@example.com" };

        var result = service.GetAvatar(context);

        Assert.Equal("http://www.gravatar.com/avatar/55502f40dc8b7c769880b10874abc9d0?s=80&r=g", result);
    }

    private static GravatarService CreateService(GravatarOptions options) =>
        new(Options.Create(options));
}
