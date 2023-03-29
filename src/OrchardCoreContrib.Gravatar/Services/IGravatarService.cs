namespace OrchardCoreContrib.Gravatar.Services;

public interface IGravatarService
{
    string GetAvatarUrl(string email, int size = GravatarConstants.DefaultSize);
}
