using Microsoft.AspNetCore.Razor.TagHelpers;
using OrchardCoreContrib.Gravatar.Services;
using System;
using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.Gravatar.TagHelpers;

[HtmlTargetElement("gravatar", TagStructure = TagStructure.NormalOrSelfClosing)]
public class GravatarTagHelper : TagHelper
{
    private readonly IGravatarService _gravatarService;

    public GravatarTagHelper(IGravatarService gravatarService)
    {
        _gravatarService = gravatarService;
    }

    public string Email { get; set; }

    [Range(1, 512)]
    public int Size { get; set; } = GravatarConstants.DefaultSize;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "img";

        var avatarUrl = _gravatarService.GetAvatarUrl(Email, Size);

        output.Attributes.Add("src", avatarUrl);
    }
}
