using Microsoft.AspNetCore.Razor.TagHelpers;
using OrchardCoreContrib.Avatars;
using System.ComponentModel.DataAnnotations;

namespace OrchardCoreContrib.Gravatar.TagHelpers;

[HtmlTargetElement("gravatar", TagStructure = TagStructure.NormalOrSelfClosing)]
public class GravatarTagHelper(IAvatarService avatarService) : TagHelper
{
    public string Email { get; set; }

    [Range(1, 512)]
    public int Size { get; set; } = GravatarConstants.DefaultSize;

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        output.TagName = "img";

        var avatarUrl = avatarService.GetAvatar(new AvatarContext { Email = Email }, Size);

        output.Attributes.Add("src", avatarUrl);
    }
}
