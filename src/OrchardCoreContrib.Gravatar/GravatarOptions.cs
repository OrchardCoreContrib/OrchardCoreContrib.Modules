using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.Gravatar;

public class GravatarOptions
{
    public string DefaultImage { get; set; }

    public GravatarRating Rating { get; set; } = GravatarRating.PG;

    [Range(1, 512)]
    public int Size { get; set; } = 24;
}
