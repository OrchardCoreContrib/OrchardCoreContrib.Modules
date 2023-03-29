using Microsoft.Extensions.Options;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace OrchardCoreContrib.Gravatar.Services;

public class GravatarService : IGravatarService
{
    private const string GravatarUrl = "http://www.gravatar.com/avatar/";

    private readonly GravatarOptions _gravatarOptions;

    public GravatarService(IOptions<GravatarOptions> gravatarOptions)
    {
        _gravatarOptions = gravatarOptions.Value;
    }

    public string GetAvatarUrl(string email, [Range(1, 512)] int size = GravatarConstants.DefaultSize)
    {
        if (string.IsNullOrEmpty(email))
        {
            throw new ArgumentException($"'{nameof(email)}' cannot be null or empty.", nameof(email));
        }

        var hash = ComputeHash(email);

        var gravatarImageUrl = $"{GravatarUrl}{hash}?s={size}&r={_gravatarOptions.Rating}";

        if (!String.IsNullOrEmpty(_gravatarOptions.DefaultImage))
        {
            gravatarImageUrl += $"&d={_gravatarOptions.DefaultImage}";
        }

        return gravatarImageUrl;
    }

    private static string ComputeHash(string email)
    {
        var md5 = MD5.Create();
        var bytes = Encoding.ASCII.GetBytes(email);
        var hash = md5.ComputeHash(bytes);
        var sb = new StringBuilder();

        for (var i = 0; i < hash.Length; i++)
        {
            sb.Append(hash[i].ToString("x2"));
        }

        return sb.ToString();
    }
}
