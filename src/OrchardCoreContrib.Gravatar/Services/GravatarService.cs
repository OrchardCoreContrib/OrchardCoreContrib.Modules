using Microsoft.Extensions.Options;
using OrchardCoreContrib.Avatars;
using OrchardCoreContrib.Infrastructure;
using System.Security.Cryptography;
using System.Text;

namespace OrchardCoreContrib.Gravatar.Services;

public class GravatarService(IOptions<GravatarOptions> gravatarOptions) : IAvatarProvider
{
    private const string GravatarUrl = "http://www.gravatar.com/avatar/";

    private readonly GravatarOptions _gravatarOptions = gravatarOptions.Value;

    public string GetAvatar(AvatarContext context, int size = 80)
    {
        Guard.ArgumentNotNull(context, nameof(context));
        Guard.ArgumentNotNullOrEmpty(context.Email, nameof(context.Email));

        var hash = ComputeHash(context.Email);

        var gravatarImageUrl = $"{GravatarUrl}{hash}?s={size}&r={_gravatarOptions.Rating.ToString().ToLower()}";

        if (!String.IsNullOrEmpty(_gravatarOptions.DefaultImage))
        {
            gravatarImageUrl += $"&d={_gravatarOptions.DefaultImage}";
        }

        return gravatarImageUrl;
    }

    private static string ComputeHash(string email)
    {
        var bytes = Encoding.ASCII.GetBytes(email);
        var hash = MD5.HashData(bytes);
        var sb = new StringBuilder();

        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }

        return sb.ToString();
    }
}
