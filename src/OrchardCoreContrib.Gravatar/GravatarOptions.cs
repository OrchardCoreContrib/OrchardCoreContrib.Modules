using System;
using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.Gravatar;

public class GravatarOptions
{
    public string DefaultImage { get; set; }

    public GravatarRating Rating { get; set; } = GravatarRating.PG;

    [Range(1, 512)]
    [Obsolete("This property has been deprecated.")]
    public int Size { get; set; } = GravatarConstants.DefaultSize;
}
